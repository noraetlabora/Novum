using Microsoft.AspNetCore.Hosting;

namespace Os.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class OsWebHostService : Microsoft.AspNetCore.Hosting.WindowsServices.WebHostService
    {
        /// <summary>
        /// 
        /// </summary>
        public OsWebHostService(IWebHost host) : base(host)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStarting(string[] args)
        {
            Nt.Logging.Log.Server.Info("OsWebHostService OnStarting");
            Program.SetArguments(args);
            base.OnStarting(args);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStarted()
        {
            Nt.Logging.Log.Server.Info("OsWebHostService OnStarted");
            base.OnStarted();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStopping()
        {
            Nt.Logging.Log.Server.Info("OsWebHostService OnStopping");
            base.OnStopping();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStopped()
        {
            Nt.Logging.Log.Server.Info("OsWebHostService OnStopped");
            base.OnStopped();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnShutdown()
        {
            Nt.Logging.Log.Server.Info("OsWebHostService OnShutdown");
            base.OnShutdown();
        }
    }
}
