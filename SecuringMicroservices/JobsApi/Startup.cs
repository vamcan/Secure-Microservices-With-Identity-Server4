using JobsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi
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
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.Authority = "https://localhost:5011";
                    opt.Audience = "jobsApi";

                });

            services.
                AddSingleton<IConfig>(Configuration.GetSection("CustomConfig")?.Get<Config>());
            AddDbContexts(services);
            //services.AddAuthorization(options => options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin")));
            services.AddControllers();
            
        }
        public void AddDbContexts(IServiceCollection services)
        {
            var debugLogging = new Action<DbContextOptionsBuilder>(opt =>
             {
#if DEBUG
                 //This will log EF-generated SQL commands to the console
                 opt.UseLoggerFactory(LoggerFactory.Create(builder =>
                 {
                     builder.AddConsole();
                 }));
                 //This will log the params for those commands to the console
                 opt.EnableSensitiveDataLogging();
                 opt.EnableDetailedErrors();
#endif
             });
            services.AddDbContext<JobsContext>(opt =>
            {
                var connectionString = Configuration.GetConnectionString("sqldb-job") ??
                "name=Job";
                opt.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure(5));
                debugLogging(opt);

            }, ServiceLifetime.Transient);


        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            TryRunMigrationsAndSeedDatabase(app);
        }

        private void TryRunMigrationsAndSeedDatabase(IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<IConfig>();
            if(config?.RunDbMigrations == true)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<JobsContext>();
            //        dbContext.Database.Migrate();
                }
            }
            if (config?.SeedDatabase == true)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<JobsContext>();
                    DatabaseInitializer.Initialize(dbContext);
                }
            }

        }
    }
    
}
