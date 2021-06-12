using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IOpinionManager : IReadOnlyOpinionManager
    {
        Task<bool> SendOpinion(Opinion opinion);
    }
}