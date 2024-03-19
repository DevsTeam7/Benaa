using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IChatHubService
    {
        Task<Chat> CreateChat(string SenderId, string ReceiverId, Guid ScedualeId);
        Task<Messages> CreateMessage(Guid ChatId, string UserId, string Message, MessagesType messagesType);
        Task<Chat> GetScheduledChat(Guid ScedualeId);
        Task<List<Messages>> GetUnreadMessages(Guid ChatId);
        Task<List<Sceduale>> GetOpenUserSceduales(string UserId);
        Task MarkMessageRead(Guid MessageId);
    }
}