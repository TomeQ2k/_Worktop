using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worktop.Data;
using Worktop.Core.Filters;
using Worktop.Core.Services;
using Worktop.Core.Services.Interfaces;
using Worktop.Core.SignalR;
using Worktop.Core.Settings;
using Worktop.Models.Domain;
using Worktop.Core.Helpers;
using Worktop.BackgroundServices;
using Worktop.BackgroundServices.Interfaces;

namespace Worktop
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminPolicy, policy => policy.RequireClaim(ClaimTypes.Role, Constants.AdminRole));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = Routes.LoginRoute;
                options.LogoutPath = Routes.LogoutRoute;
                options.AccessDeniedPath = Routes.AccessDeniedRoute;
            });

            services.AddMvc(options =>
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

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString(AppSettingsKeys.ConnectionString));
            });

            services.AddOptions();

            #region services

            services.AddScoped<IDatabase, Database>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IResetPasswordManager, ResetPasswordManager>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ITasksManager, TasksManager>();
            services.AddScoped<IPanelManager, PanelManager>();
            services.AddScoped<IOpinionManager, OpinionManager>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IMailboxService, MailBoxService>();
            services.AddScoped<IMessenger, Messenger>();
            services.AddScoped<IChatRoomsManager, ChatRoomsManager>();
            services.AddScoped<IStorageManager, StorageManager>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<IDirectoryManager, DirectoryManager>();
            services.AddScoped<IFilePathBuilder, FilePathBuilder>();
            services.AddScoped<IStorageSizeManager, StorageSizeManager>();
            services.AddScoped<IConnectionManager, ConnectionManager>();
            services.AddScoped<IJobService, JobService>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IFileWriter, FileWriter>();
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<ICookieClaimsPrincipalManager, CookieClaimsPrincipalManager>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IMimeMappingService>(new MimeMappingService(new FileExtensionContentTypeProvider()));

            services.AddSingleton<Alertify>(a => Alertify.Build());

            #endregion

            #region settings

            services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));

            #endregion

            #region filters

            services.AddScoped<OnlyAnonymousFilter>();

            #endregion

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
