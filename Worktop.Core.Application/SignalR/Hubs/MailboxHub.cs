using System;
using System.Threading.Tasks;

namespace Worktop.Core.Application.SignalR.Hubs
{
    public class MailboxHub : BaseHub
    {
        public MailboxHub(IConnectionManager connectionManager) : base(connectionManager) { }

        public async override Task OnConnectedAsync()
        {
            await connectionManager.StartConnection(Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await connectionManager.CloseConnection(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}