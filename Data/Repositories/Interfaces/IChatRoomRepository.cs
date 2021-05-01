using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories.Interfaces
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task<ChatRoom> FindRoomWithConnections(string id);
        Task<ChatRoom> FindRoomWithConnectionsAndMessages(string id);

        Task<IEnumerable<ChatRoom>> GetRoomsWithConnections();
    }
}