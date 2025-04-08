using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationByIdAsync(Guid id);
        Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId);
        Task AddNotificationAsync(Notification notification);
        Task<bool> UserExistsAsync(Guid userId);
        Task DeleteNotificationAsync(Guid notificationId);
        Task UpdateNotificationAsync(Notification notification);
    }
}
