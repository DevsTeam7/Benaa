namespace Benaa.Core.Interfaces.Hubs
{
    public interface INotificationHub
    {
        public Task SendNotification(string notification);
    }
}
