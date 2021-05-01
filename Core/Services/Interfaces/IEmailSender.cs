using System.Threading.Tasks;
using Worktop.Models.Helpers.Email;

namespace Worktop.Core.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> Send(EmailMessage emailMessage);
    }
}