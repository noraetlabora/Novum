using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Resources;
using System.ServiceProcess;

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
        public static OsServerConfiguration Configuration { get; private set; }
        public static ResourceManager Dictionary { get; private set; }
        private static Client.ClientApi _clientApi;

        private static string configFile = AppDomain.CurrentDomain.BaseDirectory + @"\Os.Server.config.json";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Os.Server  ==================================================================");
                GetConfig();
                SetClientApi();
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
                        options.ListenAnyIP((int)Configuration.OsServerPort, listenOptions =>
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

        private static void GetConfig()
        {
            if (!System.IO.File.Exists(configFile))
            {
                throw new Exception("couldn't find configuration file " + configFile);
            }

            Configuration = new Os.Server.OsServerConfiguration();
            var json = System.IO.File.ReadAllText(configFile);
            Configuration = System.Text.Json.JsonSerializer.Deserialize<Os.Server.OsServerConfiguration>(json);
            Configuration.DatabasePassword = Nt.Util.Encryption.DecryptString(Configuration.DatabasePassword);
            Nt.Logging.Log.Server.Info("Configuration : " + Configuration.ToString());
        }

        private static void SetClientApi()
        {
            _clientApi = new Client.ClientApi(string.Format("http://{0}:{1}", Configuration.OsClientIp, Configuration.OsClientPort));
        }
    }
}
