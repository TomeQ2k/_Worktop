using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Application.Services;
using Worktop.Infrastructure.Shared.Services;

namespace Worktop.WebApp.AppConfigs
{
    public static class SingletonServicesAppConfig
    {
        public static IServiceCollection ConfigureSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IFileWriter, FileWriter>();
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<ICookieClaimsPrincipalManager, CookieClaimsPrincipalManager>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IMimeMappingService>(new MimeMappingService(new FileExtensionContentTypeProvider()));

            services.AddSingleton<Alertify>(a => Alertify.Build());

            return services;
        }
    }
}