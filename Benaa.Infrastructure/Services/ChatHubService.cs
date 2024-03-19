using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.Extensions.Logging;

namespace Benaa.Infrastructure.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly ISchedualRepository _schedualRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        private readonly ILogger<ChatHubService> _logger;

        public ChatHubService(ISchedualRepository schedualRepository,
            IChatRepository chatRepository,
            IMessageRepository messageRepository,
            ILogger<ChatHubService> logger)
        {
            _schedualRepository = schedualRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task<Chat> CreateChat(string SenderId, string ReceiverId, Guid ScedualeId)
        {
            var chat = new Chat
            {
                SenderId = SenderId,
                ReceiverId = ReceiverId,
                ScedualeId = ScedualeId
            };
            var createdChat = await _chatRepository.Create(chat);
            await _chatRepository.SaveChangeAsync();
            return createdChat;
        }

        public async Task<Messages> CreateMessage(Guid ChatId, string UserId, string Message, MessagesType messagesType)
        {
            var message = new Messages
            {
                UserId = UserId,
                Message = Message,
                Type = messagesType,
                ChatId = ChatId
            };
            var createdMessage = await _messageRepository.Create(message);
            await _messageRepository.SaveChangeAsync();
            return createdMessage;
        }

        public async Task<List<Sceduale>> GetOpenUserSceduales(string UserId)
        {
            return await _schedualRepository.Select(sceduale => (sceduale.TeacherId == UserId
                || sceduale.StudentId == UserId)
                && sceduale.Status == ScedualeStatus.Opened);
        }

        public async Task<Chat> GetScheduledChat(Guid ScedualeId)
        {
            return await _chatRepository.FirstAsync(chat => chat.ScedualeId == ScedualeId);
        }

        public async Task<List<Messages>> GetUnreadMessages(Guid ChatId)
        {
            return await _messageRepository.Select(message => message.ChatId == ChatId && !message.IsRead);

        }

        public async Task MarkMessageRead(Guid MessageId)
        {
            var message = await _messageRepository.GetById(MessageId);
            message.IsRead = true;
            await _messageRepository.Update(message);
        }
    }
}
