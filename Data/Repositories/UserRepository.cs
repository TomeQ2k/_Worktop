using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Enums;
using Worktop.Data.Repositories.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<User> GetUserWithJob(int userId)
            => await context.Users
                .Include(u => u.Job)
                .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task<User> GetWorker(int userId)
            => await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Job)
                .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task<User> FindUserToLogin(string email)
            => await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        public async Task<IEnumerable<User>> GetWorkers()
            => await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Job)
                .ToListAsync();

        public async Task<IEnumerable<User>> GetFilteredWorkers(string userName, string email, WorkersSortType sortType)
        {
            var workers = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(userName))
                workers = workers.Where(w => w.UserName.ToLower().Contains(userName.ToLower()));

            if (!string.IsNullOrEmpty(email))
                workers = workers.Where(w => w.Email.ToLower().Contains(email.ToLower()));

            workers = sortType switch
            {
                WorkersSortType.None => workers,
                WorkersSortType.SalaryAscending => workers.OrderBy(w => w.Job.Salary * (decimal)w.SalaryBonus),
                WorkersSortType.SalaryDescending => workers.OrderByDescending(w => w.Job.Salary * (decimal)w.SalaryBonus),
                WorkersSortType.DateHiredAscending => workers.OrderBy(w => w.DateHired),
                WorkersSortType.DateHiredDescending => workers.OrderByDescending(w => w.DateHired),
                _ => workers
            };

            return await workers.Include(u => u.UserRoles)
                                    .ThenInclude(ur => ur.Role)
                                .Include(u => u.Job)
                                .ToListAsync();
        }
    }
}