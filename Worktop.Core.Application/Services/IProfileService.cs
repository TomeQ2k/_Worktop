using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
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