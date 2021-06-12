using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyOpinionManager
    {
        Task<IEnumerable<Opinion>> FetchOpinions();
    }
}