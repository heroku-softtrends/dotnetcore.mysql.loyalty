using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Loyalty.Models;
using Microsoft.EntityFrameworkCore;


namespace Loyalty
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc();
            //services.AddDistributedMemoryCache();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //});
            //services.AddMemoryCache();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //});
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(300);
            });
            services.AddSession();

            services.AddMvc();

            string _connectionString = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
            _connectionString.Replace("//", "");

            char[] delimiterChars = { '/', ':', '@', '?' };
            string[] strConn = _connectionString.Split(delimiterChars);
            strConn = strConn.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Config.User = strConn[1];
            Config.Pass = strConn[2];
            Config.Server = strConn[3];
            Config.Database = strConn[4];
            Config.Port = "3306";
            Config.ConnectionString = "host=" + Config.Server + ";port=" + Config.Port + ";database=" + Config.Database + ";uid=" + Config.User + ";pwd=" + Config.Pass + ";";

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSession();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=Login}/{id?}");
            });

        }
    }
}
