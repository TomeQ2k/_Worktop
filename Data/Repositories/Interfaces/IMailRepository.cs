using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories.Interfaces
{
    public interface IMailRepository : IRepository<Mail>
    {
        Task<IEnumerable<Mail>> GetFilteredMails(int currentUserId, string subject, bool onlyFavorites, MailsSortType sortType);
    }
}