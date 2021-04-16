using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace practice_middleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async(context,next)=>
            {
                await context.Response.WriteAsync("<br>Middleware1---------------</br>");
                context.Items["isVerified"]=true;
                await next.Invoke();
            });
            app.Use(async(context,next)=>{
                
                await context.Response.WriteAsync($"<br>Middleware2----------------Middleware1.Value:{context.Items["isVerified"]}</br>");
                context.Items["action"]="m2 already done";
                await next.Invoke();
            });
            app.Use(async(context,next)=>{

                await context.Response.WriteAsync($"<br>Middleware3----------------Middleware2.Value:{context.Items["action"]}</br>");
                context.Items["message"]="m3 already done";
                await next.Invoke();
            });
            app.Use(async(context,next)=>{

                await context.Response.WriteAsync($"<br>Middleware4----------------Middleware3.Value:{context.Items["message"]}</br>");
            });

        }
    }
}
