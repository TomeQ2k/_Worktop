using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IAuthService : IReadOnlyAuthService
    {
        Task<User> Login(string email, string password);
        Task<User> Register(string email, string username, string password);

        Task<bool> ConfirmAccount(string email, string token);

        Task Logout();
    }
}