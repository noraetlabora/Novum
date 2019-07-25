using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Novum.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Nt.Database.DB.Instance.ConnectionString = "Server=192.168.0.4; Port=1972; Namespace=PROG-DEV; User ID=_SYSTEM; Password=SYS";
            Nt.Database.DB.Instance.Open();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:50051");
                });
    }
}
