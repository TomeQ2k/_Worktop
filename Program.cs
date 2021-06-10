using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Worktop.Data;
using Worktop.BackgroundServices.Interfaces;
using System.Threading.Tasks;

namespace Worktop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    var databaseManager = services.GetRequiredService<IDatabaseManager>();

                    context.Database.Migrate();
                    logger.LogInformation("Database seed completed");

                    await databaseManager.Seed();

                    host.Run();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occured during migration");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
