using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Data.Repositories.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(DataContext context) : base(context) { }

        public async Task<ChatRoom> FindRoomWithConnections(string chatRoomId)
            => await context.ChatRooms
                .Include(c => c.Connections)
                .FirstOrDefaultAsync(c => c.Id == chatRoomId);

        public async Task<ChatRoom> FindRoomWithConnectionsAndMessages(string chatRoomId)
            => await context.ChatRooms
                .Include(c => c.Connections)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatRoomId);

        public async Task<IEnumerable<ChatRoom>> GetRoomsWithConnections()
            => await context.ChatRooms.Include(c => c.Connections)
                .ToListAsync();
    }
}