using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Helpers.Pagination;
using Worktop.Core.Params;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
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