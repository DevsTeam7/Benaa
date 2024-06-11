using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.Hubs;
using Benaa.Core.Interfaces.IServices;


namespace Benaa.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub, IChatHub
    {
        private string userId;
        private readonly IChatHubService _chatHubService;
        public ChatHub( IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public override async Task OnConnectedAsync()
        {
            userId = Context.UserIdentifier;
            //Get all open sceduale for current usser
            var sceduales = await _chatHubService.GetOpenUserSceduales(userId);

            foreach (var userSceduale in sceduales)
            {
                //Get a single chat connected to a single sceduale
                var chat = await _chatHubService.GetScheduledChat(userSceduale.Id);

                if (chat is not null)
                {
                    //to load all unread messages 
                    var unreadMessages = await _chatHubService.GetUnreadMessages(chat.Id);
                    if (unreadMessages is not null)
                    {
                        foreach (var message in unreadMessages)
                        {
                            //the idea is to sent a meassage to specifc chat with out sinding it to the group agin
                            // send all messages 
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

        //How to deel with mulitMida?? vid, pic and voice note if you can
        public async Task SendMessage(string message, string groupName)
        {
            await AddToGroup(groupName);
            var createdMessage =  await _chatHubService.CreateMessage(Guid.Parse(groupName), Context.UserIdentifier, message, MessagesType.Text);
            if (createdMessage is not null)
                await Clients.OthersInGroup(createdMessage.ChatId.ToString()).SendAsync("ReceiveMessage", createdMessage.Message);
            else
                await Clients.Caller.SendAsync("FaildToSendMessage", "The message was not sent try again");
        }

        public async Task MarkMessageAsRead(string messageId)
        {
            //TODO : Check if the message exist first + make the ids list
            await _chatHubService.MarkMessageRead(Guid.Parse(messageId));
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("SendToGroup", $"{Context.ConnectionId} has joined the chat {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("SendToGroup", $"{Context.ConnectionId} has left the chat {groupName}.");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Debug.WriteLine($"[Benaa's WebSocket System] Disconnected -> ${Context.ConnectionId}");
            await Clients.Caller.SendAsync("[Benaa] Connected successfully!");
        }
    }
}
