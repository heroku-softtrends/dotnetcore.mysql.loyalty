using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Loyalty.Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var config = new ConfigurationBuilder()
            // .AddEnvironmentVariables(prefix: "ASPNETCORE_")
            //.AddJsonFile("hosting.json", optional: true)
            // .Build();

            //var host = new WebHostBuilder().UseConfiguration(config)
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseUrls(string.Format("{0}", args[0]))
            //    .Build();

            //host.Run();

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .UseUrls(string.Format("{0}", args[0]))
               .Build();

            host.Run();
        }
    }
}
