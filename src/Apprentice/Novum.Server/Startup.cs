using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Novum.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                // This registers a global interceptor with a Singleton lifetime. The interceptor must be added to the service collection in addition to being registered here.
                options.Interceptors.Add<LoggerInterceptor>();
            });
            services.AddSingleton(new LoggerInterceptor());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Communication with gRPC endpoints must be made through a gRPC client.
                // To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
                endpoints.MapGrpcService<Services.SystemService>();
                endpoints.MapGrpcService<Services.StaticDataService>();
                endpoints.MapGrpcService<Services.RuntimeDataService>();
                endpoints.MapGrpcService<Services.AuthenticationService>();
            });
        }
    }
}
