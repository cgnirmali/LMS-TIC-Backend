using LMS.Assets.Enums;

namespace LMS.DTOs.ResponseModel
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid ReceiverId { get; set; }

        public string Message { get; set; }

        public NotificationType NotificationType { get; set; }
    }
}
