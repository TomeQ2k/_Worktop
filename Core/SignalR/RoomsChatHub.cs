using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Worktop.Core.Extensions;
using Worktop.Models.Domain;
using Worktop.Core.Helpers;

namespace Worktop.Core.SignalR
{
    public class RoomsChatHub : BaseHub
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private static string roomId;

        public RoomsChatHub(IConnectionManager connectionManager, IHttpContextAccessor httpContextAccessor) : base(connectionManager)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task SendMessage(Message message, string roomId)
            => await Clients.Group(roomId).SendAsync(SignalrActions.MESSAGE_RECEIVED, message, roomId);

        public async override Task OnConnectedAsync()
        {
            var currentRoomId = roomId ?? Utils.Id();

            await connectionManager.StartConnection(Context.ConnectionId, currentRoomId);

            await Groups.AddToGroupAsync(Context.ConnectionId, currentRoomId);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            roomId = null;

            var connectionsToClose = await connectionManager.FetchConnections(httpContextAccessor.HttpContext.GetCurrentUserId());

            foreach (var connectionToClose in connectionsToClose)
                await connectionManager.CloseConnection(connectionToClose.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public static void SetRoomId(string currentRoomId)
        {
            roomId = currentRoomId;
        }
    }
}