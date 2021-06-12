using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worktop.Core.Application.SignalR.Hubs;
using Worktop.Core.Common.Helpers;
using Worktop.WebApp.AppConfigs;

namespace Worktop.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.ConfigureIdentity();

            services.ConfigureAuthentication(Configuration);
            services.ConfigureAuthorization();

            services.ConfigureCookies();

            services.ConfigureMvc();

            services.ConfigureDbContext(Configuration);

            services.AddOptions();

            #region services

            services.ConfigureScopedServices();
            services.ConfigureSingletonServices();

            #endregion

            services.ConfigureSettings(Configuration);

            services.ConfigureFilters();

            services.AddSignalR();

            services.AddAutoMapper(typeof(Startup));

            services.AddDataProtection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler(Routes.ErrorRoute);

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider()
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<MailboxHub>(Routes.MailboxHubRoute);
                endpoints.MapHub<GlobalChatHub>(Routes.GlobalChatHubRoute);
                endpoints.MapHub<RoomsChatHub>(Routes.RoomsChatHubRoute);
            });

            StorageLocation.Init(Configuration.GetValue<string>(AppSettingsKeys.ServerAddress));
        }
    }
}