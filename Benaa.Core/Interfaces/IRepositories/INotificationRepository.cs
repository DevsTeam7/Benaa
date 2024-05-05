using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        bool MarkNotificationAsRead(Guid notificationId);
    }
}
