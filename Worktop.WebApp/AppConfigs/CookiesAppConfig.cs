using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.AppConfigs
{
    public static class CookiesAppConfig
    {
        public static IServiceCollection ConfigureCookies(this IServiceCollection services)
            => services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = Routes.LoginRoute;
                options.LogoutPath = Routes.LogoutRoute;
                options.AccessDeniedPath = Routes.AccessDeniedRoute;
            });
    }
}