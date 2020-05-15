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
        public static ServerConfiguration serverConfiguration { get; private set; }

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
                serverConfiguration = new ServerConfiguration();
                serverConfiguration.BookingSystem = BookingSystemType.SVS;
                serverConfiguration.Port = 5000;
                serverConfiguration.Address = "https://webservices-cert.storedvalue.com/svsxml/v1/services/SVSXMLWay";  //TEST
                //serverConfiguration.Address = "https://webservices.storedvalue.com/svsxml/v1/services/SVSXMLWay"; //PRODUCTION
                serverConfiguration.Username = "BRGEG-cert";
                serverConfiguration.Password = "ier2Ela@sea7Te";
                serverConfiguration.Timeout = 10;
                serverConfiguration.Arguments = new System.Collections.Generic.List<string>();
                serverConfiguration.Arguments.Add("5045076327250000000"); //Routing Id
                serverConfiguration.Arguments.Add("509139"); //Merchant Id
                serverConfiguration.Arguments.Add("Breuninger Hospitality"); //Merchant Name

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

        private static void GetConfig()
        {
            if (!System.IO.File.Exists(serverConfigFile))
            {
                throw new Exception("couldn't find configuration file " + serverConfigFile);
            }

            serverConfiguration = new ServerConfiguration();
            var json = System.IO.File.ReadAllText(serverConfigFile);
            serverConfiguration = System.Text.Json.JsonSerializer.Deserialize<ServerConfiguration>(json);
            Nt.Logging.Log.Server.Info("ServerConfiguration : " + serverConfiguration.ToString());
        }
    }
}