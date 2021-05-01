using System.Threading.Tasks;
using Worktop.Models.Helpers.Pagination;
using Worktop.Core.Params;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IMessenger
    {
        Task<PagedList<Message>> GetMessages(FetchMessagesParams filterParams);

        Task<Message> Send(string text, string senderName, string roomId = null);
    }
}