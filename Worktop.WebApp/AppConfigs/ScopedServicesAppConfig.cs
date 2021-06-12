using Microsoft.Extensions.DependencyInjection;
using Worktop.Core.Application.Services;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Application.SignalR;
using Worktop.Core.Domain.Data;
using Worktop.Infrastructure.Persistence.Database;
using Worktop.Infrastructure.Shared.Services;
using Worktop.Infrastructure.Shared.SignalR;
using Worktop.WebApp.BackgroundServices;
using Worktop.WebApp.BackgroundServices.Interfaces;

namespace Worktop.WebApp.AppConfigs
{
    public static class ScopedServicesAppConfig
    {
        public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
        {
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

            services.AddScoped<IReadOnlyAuthService, AuthService>();
            services.AddScoped<IReadOnlyRolesService, RolesService>();
            services.AddScoped<IReadOnlyProfileService, ProfileService>();
            services.AddScoped<IReadOnlyTasksManager, TasksManager>();
            services.AddScoped<IReadOnlyPanelManager, PanelManager>();
            services.AddScoped<IReadOnlyOpinionManager, OpinionManager>();
            services.AddScoped<IReadOnlyArticleService, ArticleService>();
            services.AddScoped<IReadOnlyMailboxService, MailBoxService>();
            services.AddScoped<IReadOnlyMessenger, Messenger>();
            services.AddScoped<IReadOnlyChatRoomsManager, ChatRoomsManager>();
            services.AddScoped<IReadOnlyStorageManager, StorageManager>();
            services.AddScoped<IReadOnlyDirectoryManager, DirectoryManager>();
            services.AddScoped<IReadOnlyStorageSizeManager, StorageSizeManager>();

            return services;
        }
    }
}