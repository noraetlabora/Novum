using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nt.Booking.Systems;
using System;
using System.ServiceProcess;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Nt.Booking
{
    /// <summary>
    /// 
    /// </summary>
    public class NtBooking
    {

        /// <summary> </summary>
        public enum BookingSystemType
        {
            /// <summary> </summary>
            ExSI = 1,
            /// <summary> </summary>
            Gantner = 2,
            /// <summary> </summary>
            SVS = 3,
            /// <summary> </summary>
            Unknown = -1
        }

        /// <summary></summary>
        public static ServerConfiguration serverConfiguration { get; private set; }

        /// <summary> </summary>
        public static IBookingSystem BookingSystem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                var switchMappings = new Dictionary<string, string>()
                 {
                     { "-i", "input" },
                     { "--input", "input"}
                 };
                var builder = new ConfigurationBuilder();
                builder.AddCommandLine(args, switchMappings);
                var config = builder.Build();

                var serverConfigFile = config.GetValue<string>("input", AppDomain.CurrentDomain.BaseDirectory + "Nt.Booking.config.json");

                Nt.Logging.Log.Server.Info("================================================================== Nt.Booking  ==================================================================");
                Resources.Dictionary.Initialize("de-AT");

                serverConfiguration = new ServerConfiguration(serverConfigFile);

                BookingSystem = BookingSystemFactory.Create(serverConfiguration);
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
                        options.ListenAnyIP((int)serverConfiguration.Port, listenOptions =>
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
    }
}