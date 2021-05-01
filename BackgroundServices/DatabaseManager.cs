using System.Linq;
using Worktop.Core.Services.Interfaces;
using Worktop.Core.Helpers;
using Worktop.BackgroundServices.Interfaces;

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

        public void Seed()
        {
            InsertRoles();
            InsertJobs();
        }

        #region private

        private void InsertRoles()
        {
            Constants.RolesToSeed.ToList().ForEach((roleName) =>
            {
                rolesService.CreateRole(roleName).Wait();
            });
        }

        private async void InsertJobs() => await jobService.InsertJobsFromFile();

        #endregion
    }
}