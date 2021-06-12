using System.Threading.Tasks;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserWithJob(int userId);
        Task<User> GetWorker(int userId);
        Task<User> FindUserToLogin(string email);

        Task<IPagedList<User>> GetWorkers((int PageNumber, int PageSize) pagination);
        Task<IPagedList<User>> GetFilteredWorkers(string username, string email, WorkersSortType sortType, bool isAdmin, (int PageNumber, int PageSize) pagination);
    }
}