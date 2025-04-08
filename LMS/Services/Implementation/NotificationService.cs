using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class NotificationService:INotifictionService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> SendNotificationAsync(Guid receiverId, string message, NotificationType notificationType)
        {
            // Check if user exists
            if (!await _notificationRepository.UserExistsAsync(receiverId))
            {
                return false; // User does not exist
            }

            // Create a new notification
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ReceiverId = receiverId,
                Message = message,
                NotificationType = notificationType
            };

            // Save notification
            await _notificationRepository.AddNotificationAsync(notification);
            return true; // Notification sent successfully
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(Guid userId)
        {
            return await _notificationRepository.GetNotificationsByUserIdAsync(userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
        {
            return await _notificationRepository.GetNotificationByIdAsync(notificationId);
        }

        public async Task<bool> DeleteNotificationAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification == null)
            {
                return false; // Notification not found
            }

            await _notificationRepository.DeleteNotificationAsync(notificationId);
            return true; // Successfully deleted
        }

        public async Task<bool> UpdateNotificationAsync(Guid notificationId, string message, NotificationType notificationType)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification == null)
            {
                return false; // Notification not found
            }

            notification.Message = message;
            notification.NotificationType = notificationType;

            await _notificationRepository.UpdateNotificationAsync(notification);
            return true; // Successfully updated
        }
    }
}