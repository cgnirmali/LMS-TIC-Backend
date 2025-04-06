using LMS.Assets.Enums;

namespace LMS.DTOs.RequestModel
{
    public class NotificationRequest
    {
        public Guid ReceiverId { get; set; }

        public string Message { get; set; }

        public NotificationType NotificationType { get; set; }
    }
}
