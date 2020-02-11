using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Os.Server.Services
{
    internal class DatabaseService : IHostedService, IDisposable
    {
        private Timer _timer;

        public DatabaseService()
        {

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionString = new System.Text.StringBuilder();
            connectionString.Append("Server=").Append(OsServer.ServerConfiguration.DatabaseIp);
            connectionString.Append("; Port=").Append(OsServer.ServerConfiguration.DatabasePort);
            connectionString.Append("; Namespace=").Append(OsServer.ServerConfiguration.DatabaseNamespace);
            connectionString.Append("; User ID=").Append(OsServer.ServerConfiguration.DatabaseUser);
            connectionString.Append("; Password=").Append(OsServer.ServerConfiguration.DatabasePassword);
            //connectionString.Append("; Min Pool Size = 20;  Max Pool Size = 100; Connection Reset = true; Connection Lifetime = 5;");
            Nt.Database.DB.Instance.ConnectionString = connectionString.ToString();
            Nt.Database.DB.Instance.Open();

            Nt.Logging.Log.Server.Info("database connection is open");

            if (Nt.Database.DB.Instance.State != System.Data.ConnectionState.Open)
                throw new Exception("database connection is not open");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                Nt.Database.DB.Instance.Ping();
                switch (Nt.Database.DB.Instance.State)
                {
                    case System.Data.ConnectionState.Closed:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is closed");
                        Nt.Database.DB.Instance.Open();
                        break;
                    case System.Data.ConnectionState.Broken:
                        Nt.Logging.Log.Database.Warn("DatabaseService: connection is broken");
                        Nt.Database.DB.Instance.Close();
                        Nt.Database.DB.Instance.Open();
                        break;
                    case System.Data.ConnectionState.Connecting:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is connecting");
                        break;
                    case System.Data.ConnectionState.Executing:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is executing");
                        break;
                    case System.Data.ConnectionState.Fetching:
                        Nt.Logging.Log.Database.Info("DatabaseService: connection is fetching");
                        break;
                }
                _ = Os.Server.Logic.Data.CheckStaticData();
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Database.Error(ex, "DatabaseService: error while checking/reconnecting to Database");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}