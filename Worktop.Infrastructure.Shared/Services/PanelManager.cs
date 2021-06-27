using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class PanelManager : IPanelManager
    {
        private readonly IDatabase database;

        public PanelManager(IDatabase database)
        {
            this.database = database;
        }

        public async Task<User> GetWorker(int workerId) => await database.UserRepository.GetWorker(workerId);

        public async Task<PagedList<User>> GetWorkers(WorkerFiltersParams filtersParams)
            => (PagedList<User>)await database.UserRepository.GetWorkers((filtersParams.PageNumber, filtersParams.PageSize));

        public async Task<PagedList<User>> FilterWorkers(WorkerFiltersParams filtersParams)
            => (PagedList<User>)await database.UserRepository.GetFilteredWorkers(filtersParams, (filtersParams.PageNumber, filtersParams.PageSize));

        public async Task<IEnumerable<User>> GetAllWorkers() => await database.UserRepository.GetAll();

        public async Task<IEnumerable<Job>> GetJobs() => await database.JobRepository.GetAll();

        public async Task<bool> AssignJob(int jobId, int workerId)
        {
            var worker = await GetWorker(workerId);

            if (worker == null)
                return false;

            worker.JobId = jobId;

            database.UserRepository.Update(worker);

            return await database.Complete();
        }

        public async Task<bool> DeleteWorker(int workerId)
        {
            var worker = await GetWorker(workerId);

            database.UserRepository.Delete(worker);

            return await database.Complete();
        }
    }
}