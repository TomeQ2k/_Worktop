using System.Threading.Tasks;
using Serilog;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Helpers;
using Worktop.WebApp.BackgroundServices.Interfaces;

namespace Worktop.WebApp.BackgroundServices
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IRolesService rolesService;
        private readonly IJobService jobService;

        public DatabaseManager(IRolesService rolesService, IJobService jobService)
        {
            this.rolesService = rolesService;
            this.jobService = jobService;
        }

        public async Task Seed()
        {
            await InsertRoles();
            await InsertJobs();

            Log.Information("Database seed completed");
        }

        #region private

        private async Task InsertRoles()
        {
            foreach (var roleName in Constants.RolesToSeed)
                await rolesService.CreateRole(roleName);
        }

        private async Task InsertJobs() => await jobService.InsertJobsFromFile();

        #endregion
    }
}