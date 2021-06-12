using System.Threading.Tasks;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyMessenger
    {
        Task<PagedList<Message>> GetMessages(FetchMessagesParams filterParams);
    }
}