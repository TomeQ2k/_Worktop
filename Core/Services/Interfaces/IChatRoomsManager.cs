using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IChatRoomsManager
    {
        Task<ChatRoom> GetRoom(string roomId);
        Task<IEnumerable<ChatRoom>> GetRooms();

        Task<ChatRoom> JoinRoom(string roomId, string password = null);
        Task<bool> LeaveRoom(string roomId);

        Task<bool> CreateRoom(ChatRoom room);
        Task<bool> UpdateRoom(ChatRoom room);
        Task<bool> DeleteRoom(string roomId);
    }
}