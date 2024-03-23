namespace Benaa.Core.Interfaces.Hubs
{
    public interface IChatHub
    {
        Task AddToGroup(string groupName);
        Task MarkMessageAsRead(string messageId);
        Task RemoveFromGroup(string groupName);
        Task SendMessage(string message, string groupName);
    }
}