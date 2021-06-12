using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Entities;
using Worktop.Infrastructure.Persistence.Database;

namespace Worktop.WebApp.AppConfigs
{
    public static class IdentityAppConfig
    {
        public static IdentityBuilder ConfigureIdentity(this IServiceCollection services)
        {
            IdentityBuilder identityBuilder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = Constants.MinPasswordLength;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters += " ";
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), identityBuilder.Services)
                .AddEntityFrameworkStores<DataContext>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();

            return identityBuilder;
        }
    }
}