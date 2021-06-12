using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Common.Helpers;
using Worktop.Infrastructure.Persistence.Database;

namespace Worktop.WebApp.AppConfigs
{
    public static class DbContextAppConfig
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
            => services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString(AppSettingsKeys.ConnectionString));
            });
    }
}