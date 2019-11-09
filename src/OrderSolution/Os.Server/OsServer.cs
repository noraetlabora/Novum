using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.ServiceProcess;
using System.Text;

namespace Os.Server
{

    /// <summary>
    /// 
    /// </summary>
    public class OsServer
    {
        /// <summary>
        /// 
        /// </summary>
        public static OsArguments Arguments { get; private set; }
        public static ResourceManager Dictionary { get; private set; }

        private static Client.ClientApi _clientApi;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Os.Server  ==================================================================");
                Resources.Dictionary.Initialize("de-AT");
                var webHostBuilder = CreateWebHostBuilder(args);
                var webHost = webHostBuilder.Build();

                if (Debugger.IsAttached)
                {
                    Nt.Logging.Log.Server.Info("starting in console");
                    webHost.Run();
                }
                else
                {
                    Nt.Logging.Log.Server.Info("starting as service");
                    var webHostService = new Services.OsWebHostService(webHost);
                    ServiceBase.Run(webHostService);
                }
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Fatal(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// 
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP((int)Arguments.OsServerPort, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });
                        options.AllowSynchronousIO = true;
                    })
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                    })
                    .UseNLog();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void SetArguments()
        {
            try
            {
                Arguments = new Os.Server.OsArguments();
                var json = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Os.Server.config.json");
                Arguments = System.Text.Json.JsonSerializer.Deserialize<Os.Server.OsArguments>(json);
                Arguments.DatabasePassword = Nt.Util.Encryption.DecryptString(Arguments.DatabasePassword);
                Nt.Logging.Log.Server.Info("Arguments : " + Arguments.ToString());
            }
            catch (Exception ex)
            {
                Nt.Logging.Log.Server.Fatal(ex, "Error while parsing arguments");

            }
        }
    }
}
