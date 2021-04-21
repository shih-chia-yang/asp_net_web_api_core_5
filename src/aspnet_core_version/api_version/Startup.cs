using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api_version.Controllers;
using api_version.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api_version
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

            services.AddControllers();
            
            services.AddApiVersioning(options=>{
                options.ApiVersionReader=new HeaderApiVersionReader("api-version");
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=true;
                options.DefaultApiVersion= new ApiVersion(1,0);
                options.Conventions.Controller<SaleControllerV1>().HasApiVersion(new ApiVersion(1,0));
                options.Conventions.Controller<SaleControllerV2>().HasApiVersion(new ApiVersion(2,0));
                });

            services.AddVersionedApiExplorer(options=>{
                options.GroupNameFormat="'v'VVV";
                options.SubstituteApiVersionInUrl=true;
            });

            services.AddSwaggerGen(c =>
            {
                var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath=Path.Combine(AppContext.BaseDirectory,xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                // options.SwaggerEndpoint("/swagger/v1/swagger.json", "api_version v1");
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
