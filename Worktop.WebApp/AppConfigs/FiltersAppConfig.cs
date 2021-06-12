using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Application.Filters;

namespace Worktop.WebApp.AppConfigs
{
    public static class FiltersAppConfig
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<OnlyAnonymousFilter>();

            return services;
        }
    }
}