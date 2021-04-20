using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using todo.infrastructure;
using todo.infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace todo.mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(
                options => 
                options.UseInMemoryDatabase(TodoContext.DEFAULT_SCHEMA)
            );
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c=>{   
                c.SwaggerDoc("v1",new OpenApiInfo
                {
                    Version="v1",
                    Title="Todo API",
                    Description="A simple example ASP.NET Core Web API",
                    TermsOfService= new Uri("https://ulist.moe.gov.tw"),
                    Contact= new OpenApiContact{
                        Name="Chia-yang,shih",
                        Email="heipuser@yuntech.edu.tw",
                        Url= new Uri("https://ulist.moe.gov.tw/Home/Contact")
                    },
                    License = new OpenApiLicense{
                        Name="Use under M.O.E",
                        Url=new Uri("https://depart.moe.edu.tw/ed2200/"),
                    }
                });

                var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath=Path.Combine(AppContext.BaseDirectory,xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //enable middleware to serve generated Swagger as a json endpoint
            app.UseSwagger();

            //enable middleware to serve swagger-ui(HTML,JS,CSS,etc)
            //specifying the Swagger JSON endpoint
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Todo API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Todo}/{action=Index}/{id?}");
            });
        }
    }
}
