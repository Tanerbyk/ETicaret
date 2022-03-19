using E_Ticaret;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostcontext, config) =>
            {
                var env = hostcontext.HostingEnvironment;

                config.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.shared.json", true, true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true).AddEnvironmentVariables();
                if (env.IsDevelopment()) config.AddUserSecrets<Program>(true);
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<E_Ticaret.Management.Startup>();
                });
    }
}
