using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
{
    public class OpinionRepository : Repository<Opinion>, IOpinionRepository
    {
        public OpinionRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Opinion>> GetUserOpinions(int userId)
            => await context.Opinions.Where(o => o.UserId == userId)
                .OrderByDescending(o => o.DateCreated)
                .ToListAsync();
    }
}