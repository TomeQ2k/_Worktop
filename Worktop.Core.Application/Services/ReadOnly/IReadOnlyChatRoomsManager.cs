using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyChatRoomsManager
    {
        Task<ChatRoom> GetRoom(string roomId);
        Task<IEnumerable<ChatRoom>> GetRooms();
    }
}