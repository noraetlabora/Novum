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
            Nt.Database.DB.Instance.ConnectionString = connectionString.ToString();
            Nt.Database.DB.Instance.Open();

            Nt.Logging.Log.Server.Info("database connection is open");

            if (Nt.Database.DB.Instance.State != System.Data.ConnectionState.Open)
                throw new Exception("database connection is not open");

            //cache static data
            Logic.Data.GetArticles();
            Logic.Data.GetCategories();
            Logic.Data.GetModifierGroups();
            Logic.Data.GetPaymentMedia();
            Logic.Data.GetPrinters();
            Logic.Data.GetUsers();
            Nt.Database.DB.Api.Misc.GetArticleGroups();
            Nt.Database.DB.Api.Misc.GetTaxGroups();

            Nt.Logging.Log.Server.Info("static data initial cached");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                Nt.Database.DB.Instance.Ping();
                System.Diagnostics.Debug.WriteLine("DatabaseService: State = " + Nt.Database.DB.Instance.State);
                if (Nt.Database.DB.Instance.State == System.Data.ConnectionState.Broken)
                {
                    System.Diagnostics.Debug.WriteLine("DatabaseService: closing Connection");
                    Nt.Database.DB.Instance.Close();
                    System.Diagnostics.Debug.WriteLine("DatabaseService: opening Connection");
                    Nt.Database.DB.Instance.Open();
                    System.Diagnostics.Debug.WriteLine("DatabaseService: State = " + Nt.Database.DB.Instance.State);
                }
                else if (Nt.Database.DB.Instance.State == System.Data.ConnectionState.Closed)
                {
                    System.Diagnostics.Debug.WriteLine("DatabaseService: opening Connection");
                    Nt.Database.DB.Instance.Open();
                    System.Diagnostics.Debug.WriteLine("DatabaseService: State = " + Nt.Database.DB.Instance.State);
                }
                Os.Server.Logic.Data.CheckStaticData();
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