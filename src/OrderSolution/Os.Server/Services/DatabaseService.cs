using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

internal class DatabaseService : IHostedService, IDisposable
{
    private Timer _timer;

    public DatabaseService()
    {

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
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
        catch(Exception ex)
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