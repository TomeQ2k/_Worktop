using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IOpinionRepository : IRepository<Opinion>
    {
        Task<IEnumerable<Opinion>> GetUserOpinions(int userId);
    }
}