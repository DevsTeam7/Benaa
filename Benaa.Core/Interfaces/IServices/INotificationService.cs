using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IServices
{
    public interface INotificationService
    {
        Task<bool> Send(string userId, string content, SchedualeNotificationDto? notificationObject = null);
    }
}
