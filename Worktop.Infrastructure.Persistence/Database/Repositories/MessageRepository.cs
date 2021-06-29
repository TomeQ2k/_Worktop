using System.Linq;
using System.Threading.Tasks;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Message>> GetMessagesInGlobalChat((int PageNumber, int PageSize) pagination)
            => await context.Messages.Where(m => m.ChatRoomId == null)
                .OrderByDescending(m => m.DateSent)
                .ToPagedList<Message>(pagination.PageNumber, pagination.PageSize);
    }
}