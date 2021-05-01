using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worktop.Data;
using Worktop.Core.Enums;
using Worktop.Core.Extensions;
using Worktop.Models.Helpers.Pagination;
using Worktop.Core.Params;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Services
{
    public class PanelManager : IPanelManager
    {
        private readonly IDatabase database;
        private readonly IRolesService rolesService;

        public PanelManager(IDatabase database, IRolesService rolesService)
        {
            this.database = database;
            this.rolesService = rolesService;
        }

        public async Task<User> GetWorker(int workerId) => await database.UserRepository.GetWorker(workerId);

        public async Task<PagedList<User>> GetWorkers(FilterWorkersParams filterParams)
            => (await database.UserRepository.GetWorkers()).ToPagedList<User>(filterParams.PageNumber, filterParams.PageSize);

        public async Task<PagedList<User>> FilterWorkers(FilterWorkersParams filterParams)
        {
            var workers = await database.UserRepository.GetFilteredWorkers(filterParams.UserName, filterParams.Email, filterParams.SortType);

            if (filterParams.IsAdmin)
                workers = workers.Where(w => rolesService.IsPermitted(RoleName.Admin, w)).ToList();

            return workers.ToPagedList<User>(filterParams.PageNumber, filterParams.PageSize);
        }

        public async Task<IEnumerable<Job>> GetJobs() => await database.JobRepository.Fetch();

        public async Task<bool> AssignJob(int jobId, int workerId)
        {
            var worker = await GetWorker(workerId);

            if (worker == null)
                return false;

            worker.JobId = jobId;

            database.UserRepository.Update(worker);

            return await database.Complete();
        }
    }
}