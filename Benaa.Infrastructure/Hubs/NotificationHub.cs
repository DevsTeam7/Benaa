using Benaa.Core.Interfaces.Hubs;
using Benaa.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace Benaa.Infrastructure.Hubs 
{
    [Authorize]
	public class NotificationHub : Hub<INotificationHub>
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public bool MarkAsRead(string notificationId)
        {
            _notificationRepository.MarkNotificationAsRead(Guid.Parse(notificationId));
            return true;
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }
}
