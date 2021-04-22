using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sa_login.Auth;
using sa_login.Repositories;

namespace sa_login
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
            services.AddControllersWithViews();
            services.AddAuthentication("SaSecurityScheme")
                .AddCookie("SaSecurityScheme",options=>{
                    options.AccessDeniedPath= new PathString("/Security/Access");
                    options.LoginPath=new PathString("/Security/Login");
                });
            services.AddAuthorization(options=>{
                options.AddPolicy("Manager",policy=>policy.RequireClaim("CanManaged"));
                options.AddPolicy("Admin",policy=>policy.AddRequirements(new ManagerRequirement(true)));
                // options.AddPolicy("AtLeast21",policy=>policy.RequireClaim("EEE"));
            });
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddScoped<IAuthorizationHandler, ManagerRequirementHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
