using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Os.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebHostServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new CustomWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
