using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace Benaa.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        //private readonly HttpContext _context;
        //private readonly UserManager<User> _userManager;
        //public ChatHub(HttpContext context, UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //    _context = context;
        //}
        // Create a dictionary of int keys and string values
       private static IDictionary<string, string> 
            ConnetedUsers = new Dictionary<string, string>();


        public override async Task OnConnectedAsync()
        {
            //   ConnetedUsers.Add(Context!.UserIdentifier!,Context.ConnectionId);
            Debug.WriteLine($"[Benaa's WebSocket System] Connected -> ${Context.ConnectionId} The user is: {Context.UserIdentifier} the user is Auth {Context.User.Identity.IsAuthenticated}");
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string message)
        {
          //  await Clients.Clients(Context.ConnectionId).SendAsync("ReceiveMessage", "");
            await Clients.OthersInGroup("").SendAsync("ReceiveMessage", message);
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
