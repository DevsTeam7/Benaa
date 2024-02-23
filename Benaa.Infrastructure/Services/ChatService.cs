using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Infrastructure.Services
{
    public class ChatService
    {
        public Task CreateChat(string connectionId)
        {
            return Task.CompletedTask;
        }
        public Task GetChatForConnectionId(string connectionId)
        {
            return Task.CompletedTask;
        }

    }
}
