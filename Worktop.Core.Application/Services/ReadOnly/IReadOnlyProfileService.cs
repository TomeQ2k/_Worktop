using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyProfileService
    {
        Task<User> GetCurrentUser();
    }
}