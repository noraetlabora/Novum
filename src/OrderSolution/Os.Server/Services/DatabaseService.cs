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
            Nt.Database.DB.Instance.ConnectionString = string.Format("Server={0}; Port={1}; Namespace={2}; User ID={3}; Password={4}", Program.Arguments.DatabaseIp, Program.Arguments.DatabasePort, Program.Arguments.DatabaseNamespace, Program.Arguments.DatabaseUser, Program.Arguments.DatabasePassword);
            Nt.Database.DB.Instance.Open();

            //cache static data
            Logic.Data.GetArticles();
            Logic.Data.GetCategories();
            Logic.Data.GetModifierGroups();
            Logic.Data.GetPaymentMedia();
            Logic.Data.GetPrinters();
            Logic.Data.GetUsers();
            Nt.Database.DB.Api.Misc.GetArticleGroups();
            Nt.Database.DB.Api.Misc.GetTaxGroups();

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