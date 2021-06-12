using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IChatRoomsManager : IReadOnlyChatRoomsManager
    {
        Task<ChatRoom> JoinRoom(string roomId, string password = null);
        Task<bool> LeaveRoom(string roomId);

        Task<bool> CreateRoom(ChatRoom room);
        Task<bool> UpdateRoom(ChatRoom room);
        Task<bool> DeleteRoom(string roomId);
    }
}