using Grpc.AspNetCore.Server;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MYMA.GrpcDataStore.Service
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }
        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GRPCStudentService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}


//public static SslServerCredentials CreateSslServerCredentials()
//{
//    var certsPath = Environment.GetEnvironmentVariable(@"C:\Workspace\Spielwiese\Github\gitrepo\MYMA.CERT");

//    var keyCertPair = new KeyCertificatePair(
//        File.ReadAllText(Path.Combine(certsPath, "server.crt")),
//        File.ReadAllText(Path.Combine(certsPath, "server.key")));

//    var caRoots = File.ReadAllText(Path.Combine(certsPath, "ca.crt"));
//    return new SslServerCredentials(new[] { keyCertPair }, caRoots, false);
//}
