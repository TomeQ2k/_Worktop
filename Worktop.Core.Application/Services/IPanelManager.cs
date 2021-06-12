using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IPanelManager
    {
        Task<User> GetWorker(int workerId);
        Task<PagedList<User>> GetWorkers(FilterWorkersParams filterParams);
        Task<PagedList<User>> FilterWorkers(FilterWorkersParams filterParams);
        Task<IEnumerable<User>> GetAllWorkers();

        Task<IEnumerable<Job>> GetJobs();

        Task<bool> AssignJob(int jobId, int workerId);

        Task<bool> DeleteWorker(int workerId);
    }
}