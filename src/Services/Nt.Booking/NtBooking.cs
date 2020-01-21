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
    public class NtBooking
    {
        public enum BookingSystemType
        {
            ExSI,
            Gantner,
            SVS
        }

        public static ServerConfiguration ServerConfiguration { get; private set; }
        public static Systems.BookingSystemBase BookingSystem { get; private set; }
        private static string serverConfigFile = AppDomain.CurrentDomain.BaseDirectory + @"\Nt.Booking.config.json";

        public static void Main(string[] args)
        {
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Nt.Booking  ==================================================================");
                //TODO: GetConfig();
                //TODO: delete following lines
                ServerConfiguration = System.Text.Json.JsonSerializer.Deserialize<ServerConfiguration>("{\"BookingSystem\": \"ExSI\", \"Port\": 5000}");
                BookingSystem = GetBookingSystem(ServerConfiguration);
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

        private static BookingSystemBase GetBookingSystem(ServerConfiguration serverConfig)
        {
            Nt.Logging.Log.Server.Info("creating booking system " + serverConfig.BookingSystem);
            switch (serverConfig.BookingSystem)
            {
                case BookingSystemType.ExSI:
                    return new Systems.Access.ExSI.ExSI();
                case BookingSystemType.Gantner:
                    throw new NotImplementedException("booking system Gantner is not yet implemented");
                case BookingSystemType.SVS:
                    return new Systems.Voucher.SVS.SVS(serverConfig.Address, serverConfig.UserName, serverConfig.Password, serverConfig.Timeout);
                default:
                    throw new Exception("couldn't find a corresponding booking system for " + serverConfig.BookingSystem);
            }
        }
    }
}
