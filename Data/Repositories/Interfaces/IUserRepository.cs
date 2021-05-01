using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserWithJob(int userId);
        Task<User> GetWorker(int userId);
        Task<User> FindUserToLogin(string email);

        Task<IEnumerable<User>> GetWorkers();
        Task<IEnumerable<User>> GetFilteredWorkers(string username, string email, WorkersSortType sortType);
    }
}