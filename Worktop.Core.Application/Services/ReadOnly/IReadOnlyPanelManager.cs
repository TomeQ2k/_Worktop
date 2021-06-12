using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyPanelManager
    {
        Task<User> GetWorker(int workerId);
        Task<PagedList<User>> GetWorkers(WorkerFiltersParams filtersParams);
        Task<PagedList<User>> FilterWorkers(WorkerFiltersParams filtersParams);
        Task<IEnumerable<User>> GetAllWorkers();

        Task<IEnumerable<Job>> GetJobs();
    }
}