using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
{
    public interface IResetPasswordManager
    {
        Task<bool> ResetPassword(string email, string newPassword, string token);
        Task<bool> SendResetPasswordCallback(string email, string newPassword);
    }
}