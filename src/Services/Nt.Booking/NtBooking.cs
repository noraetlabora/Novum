using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nt.Booking.Systems;
using System;
using System.ServiceProcess;

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
            ExSI,
            /// <summary> </summary>
            Gantner,
            /// <summary> </summary>
            SVS
        }

        /// <summary> </summary>
        public static ServerConfiguration ServerConfiguration { get; private set; }

        /// <summary> </summary>
        public static IBookingSystem BookingSystem;

        private static string serverConfigFile = AppDomain.CurrentDomain.BaseDirectory + @"\Nt.Booking.config.json";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Nt.Booking  ==================================================================");
                //TODO: GetConfig();
                //TODO: delete static server configuration
                ServerConfiguration = new ServerConfiguration();
                ServerConfiguration.BookingSystem = BookingSystemType.SVS;
                ServerConfiguration.Port = 5000;
                ServerConfiguration.Address = "http://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay";
                ServerConfiguration.Username = "BRGEG-cert";
                ServerConfiguration.Password = "ier2Ela@sea7Te";
                ServerConfiguration.Timeout = 10;

                BookingSystem = BookingSystemFactory.Create(ServerConfiguration);
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

                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                    });

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