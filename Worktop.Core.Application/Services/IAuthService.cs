using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IAuthService
    {
        Task<User> Login(string email, string password);
        Task<User> Register(string email, string username, string password);

        Task<bool> ConfirmAccount(string email, string token);

        Task Logout();

        Task<bool> EmailExists(string email);
    }
}