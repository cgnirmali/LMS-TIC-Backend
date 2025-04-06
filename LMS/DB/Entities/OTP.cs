using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class OTP
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public required DateTime EndTime { get; set; }
        public string UserEmail { get; set; }
       
        public OtpType OtpType { get; set; }
        public string Code { get; set; }

        public Guid UserId { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
