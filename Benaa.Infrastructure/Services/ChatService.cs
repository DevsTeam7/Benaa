using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Infrastructure.Services
{
    public class ChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context) {
            _context = context;
        }
        public Task CreateChat(string StudentId , string TeachrId)
        {
            Chat chat = new Chat();
            chat.SenderId = StudentId;
            chat.ReceiverId = TeachrId;
            //TODO: the time to open the chat depoend on the time selected by the user 
            _context.Chats.Add(chat);
            return Task.CompletedTask;
        }
        public Task GetChatForConnectionId(string connectionId)
        {
            return Task.CompletedTask;
        }

    }
}
