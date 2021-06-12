using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class ChatRoomsManager : IChatRoomsManager
    {
        private readonly IDatabase database;
        private readonly IReadOnlyRolesService rolesService;

        private readonly int currentUserId;

        public ChatRoomsManager(IDatabase database, IHttpContextAccessor httpContextAccessor, IReadOnlyRolesService rolesService)
        {
            this.database = database;
            this.rolesService = rolesService;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<ChatRoom> GetRoom(string roomId) => await database.ChatRoomRepository.Find(c => c.Id == roomId && c.CreatorId == currentUserId);

        public async Task<IEnumerable<ChatRoom>> GetRooms()
            => await database.ChatRoomRepository.GetRoomsWithConnections();

        public async Task<ChatRoom> JoinRoom(string roomId, string password = null)
        {
            var room = await database.ChatRoomRepository.FindRoomWithConnectionsAndMessages(roomId);

            if (room == null)
                return null;

            if (room.IsPassword && room.Password != password)
            {
                ErrorWriter<string>.Append(roomId, "Invalid chat room password");
                return null;
            }

            if (room.MaxClients != null && room.Connections.Count + 1 > room.MaxClients)
            {
                ErrorWriter<string>.Append(roomId, $"Max clients number is: {room.MaxClients}");
                return null;
            }

            return room;
        }

        public async Task<bool> LeaveRoom(string roomId)
        {
            var room = await database.ChatRoomRepository.FindRoomWithConnections(roomId);

            if (room == null)
                return false;

            var connection = room.Connections.FirstOrDefault(c => c.UserId == currentUserId);

            if (connection == null)
                return false;

            room.Connections.Remove(connection);

            return await database.Complete();
        }

        public async Task<bool> CreateRoom(ChatRoom room)
        {
            room.CreatorId = currentUserId;

            database.ChatRoomRepository.Add(room);

            return await database.Complete();
        }

        public async Task<bool> UpdateRoom(ChatRoom room)
        {
            database.ChatRoomRepository.Update(room);

            return await database.Complete();
        }

        public async Task<bool> DeleteRoom(string roomId)
        {
            bool isAdmin = await rolesService.IsPermitted(RoleName.Admin, currentUserId);
            var room = await database.ChatRoomRepository.Find(c => c.Id == roomId && (c.CreatorId == currentUserId || isAdmin));

            database.ChatRoomRepository.Delete(room);

            return await database.Complete();
        }
    }
}