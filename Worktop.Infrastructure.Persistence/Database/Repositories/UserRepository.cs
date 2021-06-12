using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Common.Enums;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
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

        public async Task<IPagedList<User>> GetWorkers((int PageNumber, int PageSize) pagination)
            => await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Job)
                .ToPagedList<User>(pagination.PageNumber, pagination.PageSize);

        public async Task<IPagedList<User>> GetFilteredWorkers(string userName, string email, WorkersSortType sortType, bool isAdmin, (int PageNumber, int PageSize) pagination)
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

            workers = workers.Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.Role)
                             .Include(u => u.Job);

            if (isAdmin)
                workers = workers.Where(u => u.UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString(RoleName.Admin)));

            return await workers.ToPagedList<User>(pagination.PageNumber, pagination.PageSize);
        }
    }
}