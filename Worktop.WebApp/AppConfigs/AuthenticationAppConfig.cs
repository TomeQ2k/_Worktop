using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Worktop.WebApp.AppConfigs
{
    public static class AuthenticationAppConfig
    {
        public static IdentityCookiesBuilder ConfigureAuthentication(this IServiceCollection services,
            IConfiguration configuration)
            => services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();
    }
}