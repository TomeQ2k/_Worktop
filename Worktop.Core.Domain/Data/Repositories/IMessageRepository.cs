using System.Threading.Tasks;
using Worktop.Core.Domain.Data.Models;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IPagedList<Message>> GetMessagesInGlobalChat((int PageNumber, int PageSize) pagination);
    }
}