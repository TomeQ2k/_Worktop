using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task<ChatRoom> FindRoomWithConnections(string id);
        Task<ChatRoom> FindRoomWithConnectionsAndMessages(string id);

        Task<IEnumerable<ChatRoom>> GetRoomsWithConnections();
    }
}