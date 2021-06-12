using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IMailboxService : IReadOnlyMailboxService
    {
        Task<Mail> SendMail(Mail mail);

        Task<bool> ToggleFavorite(int mailId);

        Task<bool> DeleteMail(int mailId);
    }
}