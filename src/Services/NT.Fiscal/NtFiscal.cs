using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nt.Logging;
using NT.Fiscal.Systems.Factories;
using NT.Fiscal.Systems.Providers;
using System;
using System.ServiceProcess;

namespace NT.Fiscal
{
    public class NtFiscal
    {
        public static ServerConfiguration ServerConfiguration { get; private set; }
        public static ProviderConfiguration ProviderConfiguration { get; private set; }
        public static FiscalProvider FiscalProvider;

        public static void Main(string[] args)
        {
            try
            {
                Log.Server.Info("================================================================== NT.Fiscal  ==================================================================");
                GetServerConfiguration();
                GetProviderConfiguration();

                // Create Fiscal Provider
                FiscalProvider = FiscalProviderFactory.Create(ProviderConfiguration);

                var webHostBuilder = CreateWebHostBuilder(args);
                var webHost = webHostBuilder.Build();

                if (Console.IsOutputRedirected)
                {
                    Log.Server.Info("starting as service");
                    var webHostService = new Services.WebHostService(webHost);
                    ServiceBase.Run(webHostService);
                }
                else
                {
                    Log.Server.Info("starting in console");
                    webHost.Run();
                }
            }
            catch (Exception ex)
            {
                Log.Server.Fatal(ex);
            }
        }

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
                  .UseStartup<Startup>()
                  .ConfigureLogging(logging =>
                  {
                      logging.ClearProviders();
                  });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void GetServerConfiguration()
        {
            // TODO: GetConfig from file
            ServerConfiguration = new ServerConfiguration();
            ServerConfiguration.Port = 5010;
            Log.Server.Info(ServerConfiguration.ToString());
        }

        public static void GetProviderConfiguration()
        {
            // TODO: GetConfig from file
            ProviderConfiguration = new ProviderConfiguration();
            ProviderConfiguration.Provider = Systems.Enums.Provider.Efsta;
            ProviderConfiguration.Country = Systems.Enums.Country.Austria;
            ProviderConfiguration.ProviderLocation = "localhost";
            Log.Server.Info(ProviderConfiguration.ToString());
        }
    }
}