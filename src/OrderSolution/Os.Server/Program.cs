using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using CommandLine;
using CommandLine.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

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
        public static Nov.NT.POS.Fiscal.IFiscalProvider FiscalProvider;

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
            Arguments = new OsArguments();
            var parserResult = CommandLine.Parser.Default.ParseArguments<OsArguments>(args);
            parserResult.WithParsed(parsedArguments =>
            {
                Nt.Logging.Log.Server.Error("---------- arguments ----------");
                Nt.Logging.Log.Server.Error(parsedArguments.ToString());
                Arguments = parsedArguments;
            });
            parserResult.WithNotParsed(errors =>
            {
                Nt.Logging.Log.Server.Error("---------- argument error ----------");
                Nt.Logging.Log.Server.Error(HelpText.AutoBuild(parserResult).ToString());
                return;
            });

            //Database connection
            Nt.Database.DB.Instance.ConnectionString = string.Format("Server={0}; Port={1}; Namespace={2}; User ID={3}; Password={4}", Arguments.DatabaseIp, Arguments.DatabasePort, Arguments.DatabaseNamespace, Arguments.DatabaseUser, Arguments.DatabasePassword);
            Nt.Database.DB.Instance.Open();
            _clientApi = new ClientApi("http://localhost:12344");

            //Logging for fiscalization
            //var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            //log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("Nov.NT.log4net"));

            //cache static data
            //Logic.Data.GetArticles();
            //Logic.Data.GetCategories("1");
            //Logic.Data.GetModifierGroups();
            //Logic.Data.GetPaymentMedia();
            //Logic.Data.GetPrinters();
            //Logic.Data.GetUsers();

            //build and run webHost
            var webHostBuilder = CreateWebHostBuilder(args);
            var webHost = webHostBuilder.Build();

            if (Debugger.IsAttached || args.Contains("--console"))
            {
                Nt.Logging.Log.Server.Error("---------- starting Os.Server in console ----------");
                webHost.Run();
            }
            else
            {
                Nt.Logging.Log.Server.Error("---------- starting Os.Server as service ----------");
                var webHostService = new Services.OsWebHostService(webHost);
                ServiceBase.Run(webHostService);
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
                        options.ListenAnyIP(5000, listenOptions =>
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
    }
}
