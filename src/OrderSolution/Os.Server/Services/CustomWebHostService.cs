using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.ComponentModel;

namespace Os.Server
{
    [DesignerCategory("Code")]
    internal class CustomWebHostService : WebHostService
    {
        public CustomWebHostService(IWebHost host) : base(host)
        {
        }

        protected override void OnStarting(string[] args)
        {
            Nt.Logging.Log.Server.Info("starting service");
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            Nt.Logging.Log.Server.Info("started service");
        }

        protected override void OnStopping()
        {
            Nt.Logging.Log.Server.Info("stopping service");
            base.OnStopping();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Nt.Logging.Log.Server.Info("stopped service");
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}
