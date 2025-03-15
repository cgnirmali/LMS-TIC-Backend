using LMS.Assets.Enums;
using LMS.DB.Entities;

namespace LMS.DTOs.RequestModel
{
    public class MailRequest
    {
        public User? User { get; set; }

        public EmailType Type { get; set; }
        public string Otp { get; set; }
    }
}
