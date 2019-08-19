using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Nt.Logging.Log.Server.Warn("OsWebHostService OnStarting");
            Program.SetArguments(args);
            base.OnStarting(args);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStarted()
        {
            Nt.Logging.Log.Server.Warn("OsWebHostService OnStarted");
            base.OnStarted();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStopping()
        {
            Nt.Logging.Log.Server.Warn("OsWebHostService OnStopping");
            base.OnStopping();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStopped()
        {
            Nt.Logging.Log.Server.Warn("OsWebHostService OnStopped");
            base.OnStopped();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnShutdown()
        {
            Nt.Logging.Log.Server.Warn("OsWebHostService OnShutdown");
            base.OnShutdown();
        }
    }
}
