using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.SignalR
{
    public interface IConnectionManager
    {
        Task<bool> StartConnection(string connectionId, string roomId = null);
        Task<bool> CloseConnection(string connectionId);

        Task<string> GetConnectionId(int userId);

        Task<IEnumerable<Connection>> FetchConnections(int userId);
    }
}