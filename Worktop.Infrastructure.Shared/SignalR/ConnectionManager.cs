using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.SignalR;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.SignalR
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IDatabase database;

        private readonly int currentUserId;

        public ConnectionManager(IDatabase database, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<bool> StartConnection(string connectionId, string roomId = null)
        {
            Connection connection = default(Connection);

            connection = await database.ConnectionRepository.Find(c => c.UserId == currentUserId);

            if (connection != null)
                database.ConnectionRepository.Delete(connection);

            connection = Connection.Create(currentUserId, connectionId, chatRoomId: roomId);

            database.ConnectionRepository.Add(connection);

            return await database.Complete();
        }

        public async Task<bool> CloseConnection(string connectionId)
        {
            var connection = await database.ConnectionRepository.Find(c => c.ConnectionId == connectionId);

            if (connection != null)
            {
                database.ConnectionRepository.Delete(connection);

                return await database.Complete();
            }

            return true;
        }

        public async Task<string> GetConnectionId(int userId) => (await database.ConnectionRepository.Find(c => c.UserId == userId))?.ConnectionId;

        public async Task<IEnumerable<Connection>> FetchConnections(int userId) => await database.ConnectionRepository.GetWhere(c => c.UserId == userId);
    }
}