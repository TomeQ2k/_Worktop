using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.AppConfigs
{
    public static class AuthorizationAppConfig
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
            => services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.AdminRole));
            });
    }
}