using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Helpers.Pagination;
using Worktop.Core.Params;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IPanelManager
    {
        Task<User> GetWorker(int workerId);
        Task<PagedList<User>> GetWorkers(FilterWorkersParams filterParams);
        Task<PagedList<User>> FilterWorkers(FilterWorkersParams filterParams);

        Task<IEnumerable<Job>> GetJobs();

        Task<bool> AssignJob(int jobId, int workerId);

        Task<bool> DeleteWorker(int workerId);
    }
}