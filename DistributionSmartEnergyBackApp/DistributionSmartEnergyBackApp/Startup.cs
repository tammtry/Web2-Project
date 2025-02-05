using DistributionSmartEnergyBackApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using DistributionSmartEnergyBackApp.Models.Interfaces;
using DistributionSmartEnergyBackApp.Services;

namespace DistributionSmartEnergyBackApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddNewtonsoftJson();

            //services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            var server = Configuration["DatabaseServer"] ?? "mssql-server";
            var port = Configuration["DatabasePort"] ?? "1433"; // Default SQL Server port
            var user = Configuration["DatabaseUser"] ?? "SA"; // Warning do not use the SA account
            var password = Configuration["DatabasePassword"] ?? "Password1!";
            var database = Configuration["DatabaseName"] ?? "DocumentDB";

            services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer($"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}"));

            //add services here
            services.AddScoped<ILocation, LocationService>();
            services.AddScoped<IWorkRequest, WorkRequestService>();
            services.AddScoped<IWorkPlan, WorkPlanService>();
            services.AddScoped<ICall, CallService>();
            services.AddScoped<IDevice, DeviceService>();
            services.AddScoped<ISafetyDoc, SafetyDocService>();
            services.AddScoped<IIncident, IncidentService>();



            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
            });

            //services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200") // the Angular app url
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });


            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            DatabaseManagementService.MigrationInitialization(app);

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString()).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
