using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Infrastructure.Persistence.Database;
using Worktop.WebApp.BackgroundServices.Interfaces;
using Worktop.Core.Common.Helpers;
using Serilog;
using Serilog.Events;

namespace Worktop.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .WriteTo.File(Constants.LogFilesPath, rollingInterval: RollingInterval.Day)
               .WriteTo.Seq("http://localhost:5000")
               .CreateLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Log.Information("Application started...");

                    var context = services.GetRequiredService<DataContext>();
                    var databaseManager = services.GetRequiredService<IDatabaseManager>();

                    await context.Database.MigrateAsync();
                    Log.Information("Database migration completed");

                    await databaseManager.Seed();

                    Log.Information("Application initialized");

                    host.Run();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Application terminated unexpectedly...");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog();
    }
}
