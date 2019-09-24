using CommandLine;
using CommandLine.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Os.Server
{

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static OsArguments Arguments { get; private set; }

        private static ClientApi _clientApi;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                //build and run webHost
                var webHostBuilder = CreateWebHostBuilder(args);
                var webHost = webHostBuilder.Build();

                if (Debugger.IsAttached || args.Contains("--console"))
                {
                    Nt.Logging.Log.Server.Info("================================================================== STARTING Os.Server IN CONSOLE ==================================================================");
                    SetArguments(args);
                    webHost.Run();
                }
                else
                {
                    Nt.Logging.Log.Server.Info("================================================================== STARTING Os.Server AS SERVICE ==================================================================");
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
        public static void SetArguments(string[] args)
        {
            Arguments = new OsArguments();
            var parserResult = CommandLine.Parser.Default.ParseArguments<OsArguments>(args);
            parserResult.WithParsed(parsedArguments =>
            {
                Nt.Logging.Log.Server.Info("================================================================== ARGUMENTS ==================================================================");
                Nt.Logging.Log.Server.Info(parsedArguments.ToString());
                Arguments = parsedArguments;
                _clientApi = new ClientApi(string.Format("http://{0}:{1}", Arguments.OsClientIp, Arguments.OsClientPort));
            });
            parserResult.WithNotParsed(errors =>
            {
                Nt.Logging.Log.Server.Error("================================================================== ARGUMENT ERROR ==================================================================");
                var builder = new StringBuilder();
                foreach (var arg in args)
                {
                    builder.Append(arg).Append(" ");
                }
                Nt.Logging.Log.Server.Error("argumenlist: " + builder.ToString());
                Nt.Logging.Log.Server.Error(HelpText.AutoBuild(parserResult).ToString());
                throw new ArgumentException();
            });
        }
    }
}
