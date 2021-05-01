using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
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