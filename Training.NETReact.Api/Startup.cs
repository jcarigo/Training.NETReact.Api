using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.NETReact.Infrasctructure;
using Training.NETReact.Application;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Training.NETReact.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)  => Configuration = configuration;
        public IConfiguration Configuration { get; }
        private static SymmetricSecurityKey SIGNING_KEY;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("ClientSecret")["Key"]));

             var allowedOrigin = new string[] {
                "https://react-app-crud.azurewebsites.net/",
                "http://localhost:3000"
            };

            ////option1: requires ad login 
            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //  .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
            //  .EnableTokenAcquisitionToCallDownstreamApi(new string[] { "user.read" })
            //  .AddInMemoryTokenCaches();

            ////option2 :requires ad login 
            // Use a distributed token cache by adding:
            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration, "AzureAd")
            //        .EnableTokenAcquisitionToCallDownstreamApi(
            //            initialScopes: new string[] { "user.read" })
            //        .AddDistributedTokenCaches();
            // Then, choose your implementation.
            // For instance, the distributed in-memory cache (not cleared when you stop the app):
            //services.AddDistributedMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("ClientSecret")["Issuer"],
                        ValidAudience = Configuration.GetSection("ClientSecret")["Issuer"],
                        IssuerSigningKey = SIGNING_KEY
                    };
                });

            services.AddMvc();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "JwtBearer";
            //    options.DefaultChallengeScheme = "JwtBearer";
            //}).AddJwtBearer("JwtBearer", jwtOptions =>
            //{
            //    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        IssuerSigningKey = SIGNING_KEY,
            //        ValidateIssuer = true, 
            //        ValidateAudience = true,
            //        ValidIssuer = Configuration.GetSection("ClientSecret")["Issuer"],
            //        ValidAudience = Configuration.GetSection("ClientSecret")["Issuer"],
            //        ValidateLifetime = true
            //    };
            //});
            services.AddCors(x => x.AddDefaultPolicy(builder =>
                builder.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            ));
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("React App Web Api.");
                });
            });
        }
    }
}
