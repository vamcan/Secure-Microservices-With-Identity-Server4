using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MvcWebClient.Config;
 
using MvcWebClient.Http;
using MvcWebClient.Infrastructure;
using MvcWebClient.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebClient
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
            services.AddHttpContextAccessor();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            
            //services.AddSingleton<IHttpClient, GeneralHttpClient>();
            

            services.AddHttpClient<IJobService, JobService>() 
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            
            services.Configure<ApiConfig>(Configuration.GetSection("ApiSettings"));
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            opt=>
            {
                opt.AccessDeniedPath = "/Auth/AccessDenied";
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
            {
                opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.Authority = "https://localhost:5011";
                opt.ClientId = "mvc-client";
                opt.ResponseType = OpenIdConnectResponseType.Code;
                opt.SaveTokens = true;
                opt.ClientSecret = "mvc-client-secret";
                opt.UsePkce = true;
                opt.Scope.Add("address");
                opt.ClaimActions.MapUniqueJsonKey("address", "address");
                opt.Scope.Add("roles");
                opt.ClaimActions.MapUniqueJsonKey("role", "role");
                opt.Scope.Add("jobsApi.scope");
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = JwtClaimTypes.Role
                };
                opt.GetClaimsFromUserInfoEndpoint = true;
            });
            
            
            
            
            services.AddControllersWithViews();
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
