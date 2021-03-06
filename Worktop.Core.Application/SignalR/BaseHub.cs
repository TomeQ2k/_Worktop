using Microsoft.AspNetCore.SignalR;

namespace Worktop.Core.Application.SignalR
{
    public abstract class BaseHub : Hub
    {
        protected readonly IConnectionManager connectionManager;

        public BaseHub(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }
    }
}