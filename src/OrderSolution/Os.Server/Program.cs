using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using NLog.Web;
using Os.Server.Services;

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
        /// <value></value>
        public string DatabaseIp { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabasePort  { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseNamespace  { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabaseUser  { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DatabasePassword { get; private set; }
        /// <summary>
        /// port of this service
        /// in default.json encoded in flag "posAdapterUrl"
        /// </summary>
        /// <value></value>
        public string ServerPort  { get; private set; }
        /// <summary>
        /// ip address where the OsServer runs
        /// </summary>
        /// <value></value>
        public string OsServerIp {get; private set;}
        /// <summary>
        /// server port of OrderSolutions we can call for eg. printing
        /// in default.json flag "httpApiPort"
        /// </summary>
        public string OsServerPort   { get; private set; }
        /// <summary>
        /// client port of OrderSolutions who calls
        /// in default.json in flag servicePort"
        /// </summary>
        /// <value></value>
        public string OsClientPort  { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Nov.NT.POS.Fiscal.IFiscalProvider FiscalProvider;

        private static ClientApi _clientApi;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Database connection
            Nt.Database.DB.Instance.ConnectionString = "Server=192.168.0.4; Port=1972; Namespace=PROG-DEV; User ID=_SYSTEM; Password=SYS";
            Nt.Database.DB.Instance.Open();
            _clientApi = new ClientApi("http://localhost:12344");

            //Logging for fiscalization
            //var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            //log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("Nov.NT.log4net"));

            //cache static data
            Logic.Data.GetArticles();
            Logic.Data.GetCategories("1");
            Logic.Data.GetModifierGroups();
            Logic.Data.GetPaymentMedia();
            Logic.Data.GetPrinters();
            Logic.Data.GetUsers();

            //service or console
            var isService = !(System.Diagnostics.Debugger.IsAttached || args.Contains("--console"));
            if (isService)
                args = args.Where(arg => arg != "--console").ToArray();

            //build and run webHost
            var webHostBuilder = CreateWebHostBuilder(args);
            var webHost = webHostBuilder.Build();

            if (isService)
            {
                var pathToExe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = System.IO.Directory.GetCurrentDirectory();
                pathToContentRoot = System.IO.Path.GetDirectoryName(pathToExe);
                webHost.RunAsCustomService();
            }
            else
            {
                webHost.Run();
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
            .ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });
                options.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    listenOptions.UseHttps();
                    //listenOptions.UseHttps("certificate.pfx", "certificatePassword");
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
