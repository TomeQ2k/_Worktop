using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;

namespace Worktop.Core.Application.Services
{
    public interface IProfileService : IReadOnlyProfileService
    {
        Task<bool> ChangePassword(string oldPassword, string newPassword);

        Task<bool> ChangeEmail(string newEmail, string token);
        Task<bool> SendChangeEmailCallback(string newEmail);

        Task<bool> ChangePhoneNumber(string phoneNumber);
    }
}