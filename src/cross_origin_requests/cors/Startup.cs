using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace cors
{
    public class Startup
    {
        public const string cors_middleware = "set_by_middleware_policy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors();
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("TestPolicy",
            //         builder =>
            //         {
            //             builder.WithOrigins("https://localhost:5002");
            //         });
            // });

            SetCorsAddPolicy(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "cors", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "cors v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            AddCorsAppByPolicy(app);
            // app.UseCors();
            // app.UseCors("TestPolicy");
            // app.UseCors(x => x
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());
            // app.UseCors(x => 
            // x.WithOrigins("TestPolicy")
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void SetCorsByMiddleWare(IServiceCollection services)
        {
            
            services.AddCors();
        }

        public void SetCorsAddPolicy(IServiceCollection services)
        {
            services.AddCors(options=>
            {
                options.AddPolicy(cors_middleware, 
                                builder=>
                                {
                                    builder.WithOrigins("https://localhost:5001")
                                    .SetIsOriginAllowed((host)=>true)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
                
            });
        }

        public void AddCorsAppByPolicy(IApplicationBuilder app)
        {
            app.UseCors(cors_middleware);
        }

        public void AddCorsApp(IApplicationBuilder app)
        {
            app.UseCors(builder=>builder
                .WithOrigins("https://localhost:5001")
                .SetIsOriginAllowed((host)=>true));
        }
    }
}
