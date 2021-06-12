using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IOpinionManager
    {
        Task<IEnumerable<Opinion>> FetchOpinions();

        Task<bool> SendOpinion(Opinion opinion);
    }
}