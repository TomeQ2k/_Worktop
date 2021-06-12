using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IMessenger : IReadOnlyMessenger
    {
        Task<Message> Send(string text, string senderName, string roomId = null);
    }
}