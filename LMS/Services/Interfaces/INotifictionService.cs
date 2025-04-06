using LMS.Assets.Enums;
using LMS.DB.Entities;

namespace LMS.Services.Interfaces
{
    public interface INotifictionService
    {
        Task<bool> SendNotificationAsync(Guid receiverId, string message, NotificationType notificationType);
        Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId);
        Task<Notification> GetNotificationByIdAsync(Guid notificationId);
        Task<bool> DeleteNotificationAsync(Guid notificationId);
        Task<bool> UpdateNotificationAsync(Guid notificationId, string message, NotificationType notificationType);
    }
}
