using System.Linq;
using System.Threading.Tasks;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Data.Repositories.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
{
    public class MailRepository : Repository<Mail>, IMailRepository
    {
        public MailRepository(DataContext context) : base(context) { }

        public async Task<IPagedList<Mail>> GetUserMails(int userId, (int PageNumber, int PageSize) pagination)
            => await context.Mails.Where((m => (m.SenderId == userId && !m.SenderDeleted) || (m.ReceiverId == userId && !m.ReceiverDeleted)))
                 .OrderByDescending(m => m.DateSent)
                 .ToPagedList<Mail>(pagination.PageNumber, pagination.PageSize);

        public async Task<IPagedList<Mail>> GetFilteredMails(int userId, IMailFilterParams filters, (int PageNumber, int PageSize) pagination)
        {
            var mails = context.Mails.Where(m => (m.SenderId == userId && !m.SenderDeleted) || (m.ReceiverId == userId && !m.ReceiverDeleted));

            if (!string.IsNullOrEmpty(filters.Subject))
                mails = mails.Where(m => m.Subject.ToLower().Contains(filters.Subject.ToLower()));

            if (filters.OnlyFavorites)
                mails = mails.Where(m => m.IsFavorite);

            mails = filters.SortType switch
            {
                MailsSortType.DateDescending => mails.OrderByDescending(m => m.DateSent),
                MailsSortType.DateAscending => mails.OrderBy(m => m.DateSent),
                _ => mails
            };

            return await mails.ToPagedList<Mail>(pagination.PageNumber, pagination.PageSize);
        }
    }
}