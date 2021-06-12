using System.Threading.Tasks;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyAuthService
    {
        Task<bool> EmailExists(string email);
    }
}