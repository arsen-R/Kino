using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Data;
using KinoSite.Areas.Identity.Data;
namespace KinoSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var s = host.Services.CreateScope())
            {
                var services = s.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MovieContext>();
                    DataInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var log = services.GetRequiredService<ILogger<Program>>();
                    log.LogError(ex.Message, "Error with DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
