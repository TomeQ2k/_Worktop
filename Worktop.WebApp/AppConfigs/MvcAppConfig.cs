using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Worktop.WebApp.AppConfigs
{
    public static class MvcAppConfig
    {
        public static IMvcBuilder ConfigureMvc(this IServiceCollection services)
            => services.AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddMvcOptions(options => options.EnableEndpointRouting = false);
    }
}