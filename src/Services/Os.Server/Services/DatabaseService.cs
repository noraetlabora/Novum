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
            //connectionString.Append("; Min Pool Size = 10;  Max Pool Size = 20; Connection Reset = true; Connection Lifetime = 5;");
            Nt.Logging.Log.Database.Info("setting connection string to: " + connectionString.ToString().Substring(0, 55) + "...");

            Nt.Database.DB.Instance.ConnectionString = connectionString.ToString();
            Nt.Database.DB.Instance.Initialize(OsServer.ServerConfiguration.DatabaseConnections);
            Nt.Database.DB.Instance.Open();

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                Nt.Database.DB.Instance.CheckConnection();
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