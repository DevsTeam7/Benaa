using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.Hubs;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Benaa.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Benaa.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public IHubContext<NotificationHub, INotificationHub> _notificationHub { get; }
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository, IHubContext<NotificationHub, INotificationHub> notificationHub)
        {
            _notificationRepository = notificationRepository;
            _notificationHub = notificationHub;
        }

        public async Task<bool> Send(string userId, string content)
        {
            Notification notification = new Notification{UserId = userId, Content = content};
            await _notificationRepository.Create(notification);
            await _notificationHub.Clients.User(notification.UserId).SendNotification(notification.Content);
            return true;
        }

    }
}
