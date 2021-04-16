using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace middleware
{
    public static class RunMiddlewareExtension
    {
        public static void RunMiddleware3(this IApplicationBuilder app)
        {
            app.Run(async (context) =>{
                await context.Response.WriteAsync("<br>Middleware3 - i am a Run Extension class<br/>");
            });
        }
    }
}
