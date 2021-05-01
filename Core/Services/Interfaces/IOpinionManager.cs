using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IOpinionManager
    {
        Task<IEnumerable<Opinion>> FetchOpinions();

        Task<bool> SendOpinion(Opinion opinion);
    }
}