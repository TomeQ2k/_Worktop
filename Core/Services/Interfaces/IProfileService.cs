using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IProfileService
    {
        Task<User> GetCurrentUser();

        Task<bool> ChangePassword(string oldPassword, string newPassword);

        Task<bool> ChangeEmail(string newEmail, string token);
        Task<bool> SendChangeEmailCallback(string newEmail);

        Task<bool> ChangePhoneNumber(string phoneNumber);
    }
}