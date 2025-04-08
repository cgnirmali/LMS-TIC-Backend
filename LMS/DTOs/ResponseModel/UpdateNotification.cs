using LMS.Assets.Enums;

namespace LMS.DTOs.ResponseModel
{
    public class UpdateNotification
    {
        // The unique identifier of the notification to update
        public Guid Id { get; set; }

        // The updated message for the notification
        public string Message { get; set; }

        // The updated notification type
        public NotificationType NotificationType { get; set; }
    }
}
