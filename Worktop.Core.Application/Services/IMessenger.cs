using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IMessenger
    {
        Task<PagedList<Message>> GetMessages(FetchMessagesParams filterParams);

        Task<Message> Send(string text, string senderName, string roomId = null);
    }
}