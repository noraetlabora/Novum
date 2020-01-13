using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.ServiceProcess;

namespace Nt.Access
{
    public class Program
    {

        public static ServerConfiguration ServerConfiguration { get; private set; }
        private static string serverConfigFile = AppDomain.CurrentDomain.BaseDirectory + @"\Nt.Access.config.json";

        public static void Main(string[] args)
        {
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Nt.Access  ==================================================================");
                GetConfig();
                var webHostBuilder = CreateWebHostBuilder(args);
                var webHost = webHostBuilder.Build();

                if (Console.IsOutputRedirected)
                {
                    Nt.Logging.Log.Server.Info("starting as service");
                    var webHostService = new Services.WebHostService(webHost);
                    ServiceBase.Run(webHostService);
                }
                else
                {
                    Nt.Logging.Log.Server.Info("starting in console");
                    webHost.Run();
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
                        options.ListenAnyIP((int)ServerConfiguration.Port, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });
                        options.AllowSynchronousIO = true;
                    })
                    .UseStartup<Startup>();

        private static void GetConfig()
        {
            if (!System.IO.File.Exists(serverConfigFile))
            {
                throw new Exception("couldn't find configuration file " + serverConfigFile);
            }

            ServerConfiguration = new ServerConfiguration();
            var json = System.IO.File.ReadAllText(serverConfigFile);
            ServerConfiguration = System.Text.Json.JsonSerializer.Deserialize<ServerConfiguration>(json);
            Nt.Logging.Log.Server.Info("ServerConfiguration : " + ServerConfiguration.ToString());
        }

    }    
}
