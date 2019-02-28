using list.Authorization;
using list.Managers;
using list.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace list
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
            services.AddTransient<IListManager, ListManager>();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddTransient<IListStorage, MongoDBListStorage>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Security:Authority"];
                options.Audience = Configuration["Security:Audience"];
                options.TokenValidationParameters.NameClaimType = ClaimTypes.NameIdentifier; // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
                
                // Will not work and not safe when not checking issuer
                //options.TokenValidationParameters.RoleClaimType = "scope";
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
                //options.AddPolicy("ListReader", policy => policy.RequireRole("read:lists"));
                //options.AddPolicy("ListWriter", policy => policy.RequireRole("write:lists"));
                options.AddPolicy("ListReader", policy => policy.Requirements.Add(new HasScopeRequirement("read:lists", Configuration["Security:Authority"])));
                options.AddPolicy("ListWriter", policy => policy.Requirements.Add(new HasScopeRequirement("write:lists", Configuration["Security:Authority"])));
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
