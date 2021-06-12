using System.Threading.Tasks;
using Worktop.Core.Application.Models.Email;

namespace Worktop.Core.Application.Services
{
    public interface IEmailSender
    {
        Task<bool> Send(EmailMessage emailMessage);
    }
}