using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace middleware
{
    public static class UseMiddlewareExtension
    {
        public static IApplicationBuilder UseMiddleware1(this IApplicationBuilder app)
        {
            return app.Use(async(context,next)=>{
                await context.Response.WriteAsync("<br>Middleware1 - I am an Use Extension class</br>");
                await next();
            });
        }

        public static IApplicationBuilder UseMiddleware2InClass(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware2>();
        }
    }

    public class Middleware2
    {
        private readonly RequestDelegate _next;
        public Middleware2(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<br>Middleware2 - I am a custom middleware class</br>");
            await this._next.Invoke(context);
        }
    }
}
