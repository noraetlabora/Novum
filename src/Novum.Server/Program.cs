using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Novum.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Novum.Database.DB.Instance.ConnectionString = "Server=192.168.0.4; Port=1972; Namespace=PROG-DEV; User ID=_SYSTEM; Password=SYS";
            Novum.Database.DB.Instance.Open();
            var state = Novum.Database.DB.Instance.State;

            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
             .ConfigureKestrel(options =>
             {
                 options.ListenAnyIP(5000, listenOptions =>
                 {
                     listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                 });
                 options.ListenAnyIP(5001, listenOptions =>
                 {
                     listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                     listenOptions.UseHttps();
                     //listenOptions.UseHttps("certificate.pfx", "certificatePassword");
                 });
             })
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .UseNLog()
            .Build();
    }
}
