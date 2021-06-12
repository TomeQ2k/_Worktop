using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyMailboxService
    {
        Task<PagedList<Mail>> GetMails(GetMailsParams filterParams);
        Task<PagedList<Mail>> FilterMails(GetMailsParams filterParams);

        Task<IEnumerable<string>> FetchEmailAddresses();
    }
}