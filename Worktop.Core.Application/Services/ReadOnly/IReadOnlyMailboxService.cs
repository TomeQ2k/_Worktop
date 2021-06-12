using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyMailboxService
    {
        Task<PagedList<Mail>> GetMails(MailFiltersParams filtersParams);
        Task<PagedList<Mail>> FilterMails(MailFiltersParams filtersParams);

        Task<IEnumerable<string>> FetchEmailAddresses();
    }
}