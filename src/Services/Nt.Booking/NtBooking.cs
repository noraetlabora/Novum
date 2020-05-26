using System.Text;
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
    /// NtBooking is used to create a booking service of a defined
    /// type. This booking service can be used to interact with other
    /// booking partners.
    /// </summary>
    public class NtBooking
    {
        /// <summary>Booking system types.</summary>
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

        /// <summary>Main configuration settings.</summary>
        public static ServerConfiguration serverConfiguration { get; private set; }

        /// <summary>Booking system that is created.</summary>
        public static IBookingSystem BookingSystem;

        /// <summary>
        /// Display help information.
        /// </summary>
        public static void Help()
        {
            var helpInfo = new StringBuilder();
            helpInfo.Append("\nNt.Booking\n\n");
            helpInfo.Append("-h/--help\tHelp information.\n");
            helpInfo.Append("-i/--input\tJSON formatted input configuration file path.\n");
            System.Console.WriteLine(helpInfo.ToString());
        }

        /// <summary>
        /// Main function. Entry point of the program.
        /// 
        /// -i/--input Input configuration JSON file.
        /// </summary>
        /// <param name="args">Optional arguments.</param>
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

                args = new string[2] { "--port", serverConfiguration.Port.ToString() };
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
                Help();
                Nt.Logging.Log.Server.Fatal(ex);
            }
        }

        /// <summary>
        /// Builder that creates web host.
        /// 
        /// -p/--port Port on that the webhoster is listening.
        /// </summary>
        /// <param name="args">Optional arguments.</param>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .ConfigureKestrel(options =>
                    {
                        var switchMappings = new Dictionary<string, string>()
                        {
                            { "-p", "port" },
                            { "--port", "port"}
                        };
                        var builder = new ConfigurationBuilder();
                        builder.AddCommandLine(args, switchMappings);
                        var config = builder.Build();

                        var port = config.GetValue<int>("port", 1000);

                        options.ListenAnyIP(port, listenOptions =>
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