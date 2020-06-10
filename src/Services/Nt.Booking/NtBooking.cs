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
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using Nt.Booking.Systems.Voucher.SVS;
using System.Text.Json;

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
        public static ServiceConfiguration ServiceConfig { get; private set; }

        /// <summary>Booking system that is created.</summary>
        public static IBookingSystem BookingSystem;

        /// <summary>
        /// Main function. Entry point of the program.
        /// 
        /// -i/--input      Optional. Input configuration JSON file.
        /// -?/-h/--help    Show help information.
        /// </summary>
        /// <param name="args">Optional arguments.</param>
        public static void Main(string[] args)
        {
            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);
            commandLineApplication.Description = "Creates a service that is used to handle booking queries.";
            commandLineApplication.Name = "Nt.Booking";
            commandLineApplication.FullName = "Nt.Booking";

            CommandOption input = commandLineApplication.Option("-i |--input <file>", "The service configuration file.", CommandOptionType.SingleValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            try
            {
                Nt.Logging.Log.Server.Info("================================================================== Nt.Booking  ==================================================================");

                commandLineApplication.OnExecute(() =>
                {
                    Resources.Dictionary.Initialize("de-AT");
                    string serverConfigFile = input.Value() ?? (AppDomain.CurrentDomain.BaseDirectory + "Nt.Booking.config.json");
                    ServiceConfig = new ServiceConfiguration(serverConfigFile);
                    ServiceConfig.Save(AppDomain.CurrentDomain.BaseDirectory + "Nt.Booking.config.json");

                    StartBookingService(ServiceConfig);

                    return 0;
                });
                commandLineApplication.Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.Out.Write(ex.Message);
                commandLineApplication.ShowHelp();

                Nt.Logging.Log.Server.Fatal(ex);
            }
        }

        /// <summary>
        /// Start booking service by a user defined configuration.
        /// </summary>
        /// <param name="serverConfiguration">Server configuration.</param>
        public static void StartBookingService(in ServiceConfiguration serverConfiguration)
        {
            BookingSystem = BookingSystemFactory.Create(serverConfiguration);

            string[] args = new string[2] { "--port", serverConfiguration.Port.ToString() };
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