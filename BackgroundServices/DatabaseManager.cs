using Worktop.Core.Services.Interfaces;
using Worktop.Core.Helpers;
using Worktop.BackgroundServices.Interfaces;
using System.Threading.Tasks;

namespace Worktop.BackgroundServices
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