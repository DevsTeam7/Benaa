using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;


namespace Benaa.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Debug.WriteLine($"[Benaa's WebSocket System] Connected -> ${Context.ConnectionId}, The user is: {Context.User.Identity.IsAuthenticated}");
           await Groups.AddToGroupAsync(Context.ConnectionId, "yaz");
        }
        public async Task SendMessage(string message)
        {
            Debug.WriteLine(message);
           // await Clients.OthersInGroup
            await Clients.OthersInGroup("yaz").SendAsync("ReciveMessage", message);

        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the chat {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the chat {groupName}.");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Debug.WriteLine($"[Benaa's WebSocket System] Disconnected -> ${Context.ConnectionId}");
            await Clients.Caller.SendAsync("[Benaa] Connected successfully!");
        }


    }
}
