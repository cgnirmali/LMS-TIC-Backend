using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }


        public string? Password { get; set; }

        public Role role { get; set; }

        public bool IsVerified { get; set; }

        
        
        // Navigation property
        
        public ICollection<OTP> OTP { get; set; }
    }
}
