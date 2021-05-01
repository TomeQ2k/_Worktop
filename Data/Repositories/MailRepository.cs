using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Enums;
using Worktop.Data.Repositories.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories
{
    public class MailRepository : Repository<Mail>, IMailRepository
    {
        public MailRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Mail>> GetFilteredMails(int currentUserId, string subject, bool onlyFavorites, MailsSortType sortType)
        {
            var mails = context.Mails.Where(m => (m.SenderId == currentUserId && !m.SenderDeleted) || (m.ReceiverId == currentUserId && !m.ReceiverDeleted));

            if (!string.IsNullOrEmpty(subject))
                mails = mails.Where(m => m.Subject.ToLower().Contains(subject.ToLower()));

            if (onlyFavorites)
                mails = mails.Where(m => m.IsFavorite);

            mails = sortType switch
            {
                MailsSortType.DateDescending => mails.OrderByDescending(m => m.DateSent),
                MailsSortType.DateAscending => mails.OrderBy(m => m.DateSent),
                _ => mails
            };

            return await mails.ToListAsync();
        }
    }
}