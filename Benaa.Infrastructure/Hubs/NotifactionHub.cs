using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;


namespace Benaa.Infrastructure.Hubs 
{
    [Authorize]
    public class NotifactionHub :Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("[Benaa] Connected successfully!");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Caller.SendAsync("[Benaa] DisConnected successfully!");
        }
    }
}
