using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Application.Settings;

namespace Worktop.WebApp.AppConfigs
{
    public static class SettingsAppConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

            return services;
        }
    }
}