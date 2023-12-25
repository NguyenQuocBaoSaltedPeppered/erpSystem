using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ERP.Databases;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ERP.Bases
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.PropertyNamingPolicy = null
                )
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddControllers();
            services.AddHttpClient();
            services.AddSingleton<StartupState>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:9000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
        }
    }
}