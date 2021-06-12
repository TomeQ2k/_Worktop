using System.Threading.Tasks;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IMailRepository : IRepository<Mail>
    {
        Task<IPagedList<Mail>> GetUserMails(int userId, (int PageNumber, int PageSize) pagination);
        Task<IPagedList<Mail>> GetFilteredMails(int userId, string subject, bool onlyFavorites, MailsSortType sortType, (int PageNumber, int PageSize) pagination);
    }
}