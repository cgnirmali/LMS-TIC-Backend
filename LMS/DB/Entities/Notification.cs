using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Description { get; set; }
        
    }
}
