using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Benaa.Infrastructure.Data;
using Benaa.Core.Entities.General;
using Microsoft.EntityFrameworkCore;


namespace Benaa.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private string userId;
        private Guid chatId;

        private readonly ApplicationDbContext _Dbcontext;
        public ChatHub(ApplicationDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }

        public override async Task OnConnectedAsync()
        {
            userId = Context.UserIdentifier!;

            var sceduales = await _Dbcontext.Sceduales
                .Where(sceduale => (sceduale.TeacherId == userId
                || sceduale.StudentId == userId)
                && sceduale.Status == ScedualeStatus.Opened)
                .ToListAsync();

            foreach (var userSceduale in sceduales)
            {
                var chat = await _Dbcontext.Chats
                    .FirstAsync(chat => chat.ScedualeId == userSceduale.Id);

                if (chat is not null)
                {
                    //to load all unread messages 
                    var unreadMessages = await _Dbcontext.Messages.Where(message => message.ChatId == chat.Id && !message.IsRead).ToListAsync();
                    if (unreadMessages is not null)
                    {
                        foreach (var message in unreadMessages)
                        {
                            //the idea is to sent a meassage to specifc chat with out sinding it to the group agin
                            await Clients.Caller.SendAsync("LoadMessage", message, chat.Id.ToString());
                        }
                    }
                }
            }


            Debug.WriteLine($"[Benaa's WebSocket System] Connected -> ${Context.ConnectionId} " +
              $"The user is: {Context.UserIdentifier}" +
              $" the user is Auth {Context.User!.Identity!.IsAuthenticated}");

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string message, string groupName)
        {
            //how to add mulitble type? i think i need controller to recive a Post request
            Chat chat = await _Dbcontext.Chats.FirstAsync(chat => chat.Id == Guid.Parse(groupName));
            var _message = new Messages {
                UserId = userId,
                Message = message,
                Type = MessagesType.Text,
                ChatId = chat.Id
            };
           await _Dbcontext.Messages.AddAsync(_message);
           await _Dbcontext.SaveChangesAsync();

            await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", message);
        }

        public async Task MarkMessageAsRead(string messageId)
        {
            string currentConnectionId = Context.ConnectionId;
            string userId = Context.UserIdentifier!;

            // Update the message's read status in the repository
           // await _messageRepository.MarkMessageAsReadAsync(messageId, userId);

           //await Clients.Client(senderConnectionId).SendAsync("MessageRead", messageId, currentConnectionId);
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
