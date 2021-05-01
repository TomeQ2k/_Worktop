using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Worktop.Models.Domain;
using Worktop.Core.Helpers;

namespace Worktop.Core.SignalR
{
    public class GlobalChatHub : BaseHub
    {
        public GlobalChatHub(IConnectionManager connectionManager) : base(connectionManager) { }

        public async Task SendMessage(Message message)
            => await Clients.All.SendAsync(SignalrActions.MESSAGE_RECEIVED, message);

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