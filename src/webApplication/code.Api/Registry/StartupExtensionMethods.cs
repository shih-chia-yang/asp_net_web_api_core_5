using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using code.Api.Application.Command;
using code.Api.Application.Queries;
using code.Domain.Event;
using code.Domain.Repositories;
using code.Infrastructure;
using code.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace code.Api.Registry
{
    public static class StartupExtensionMethods
    {
        public const string CorsPolicy = "UniversityPolicy";
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(control=>{
                control.AllowEmptyInputInBodyModelBinding = true;
            })
            .AddNewtonsoftJson(options=>{
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest);
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options=>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }

        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options=>{
                options.ApiVersionReader=new HeaderApiVersionReader("api-version");
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=true;
                options.DefaultApiVersion= new ApiVersion(1,0);
                });

            services.AddVersionedApiExplorer(options=>{
                options.GroupNameFormat="'v'VVV";
                options.SubstituteApiVersionInUrl=true;
            });
            return services;
        }

        public static IServiceCollection SetCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options=>
            {
                options.AddPolicy(CorsPolicy, 
                                builder=>
                                {
                                    builder.WithOrigins("https://localhost:5002")
                                    .SetIsOriginAllowed((host)=>true)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
                
            });
            return services;
        }

        /// <summary>
        /// 註冊物件至DI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentQueries, StudentQueries>();
            services.AddTransient<IRequestHandler<CreateStudentCommand, bool>, CreateStudentCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateStudentCommand, bool>, UpdateStudentCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteStudentCommand, bool>, DeleteStudentCommandHandler>();
            
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<IInstructorQueries, InstructorQueries>();
            services.AddTransient<IRequestHandler<CreateInstructorCommand, bool>, CreateInstructorCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteInstructorCommand, bool>, DeleteInstructorCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateInstructorCommand, bool>, UpdateInstructorCommandHandler>();
            return services;
        }

        public static IServiceCollection SwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    
                    Version = "v1",
                    Title = "code.Api",
                    Description="A simple example ASP.NET Core Web API",
                    TermsOfService= new Uri("https://ulist.moe.gov.tw"),
                    Contact = new OpenApiContact{
                        Name="Chia-yang,Shih",
                        Email="heipuser@yuntech.edu.tw",
                        Url=new Uri("https://ulist.moe.gov.tw/Home/Contact")
                    },
                    License =new OpenApiLicense{
                        Name="Use under M.O.E",
                        Url=new Uri("https://depart.moe.edu.tw/ed2200/")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}