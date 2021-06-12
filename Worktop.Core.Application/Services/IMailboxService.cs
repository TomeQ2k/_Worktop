using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IMailboxService
    {
        Task<PagedList<Mail>> GetMails(GetMailsParams filterParams);
        Task<PagedList<Mail>> FilterMails(GetMailsParams filterParams);

        Task<Mail> SendMail(Mail mail);

        Task<bool> ToggleFavorite(int mailId);

        Task<bool> DeleteMail(int mailId);

        Task<IEnumerable<string>> FetchEmailAddresses();
    }
}