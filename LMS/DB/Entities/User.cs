using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public Roll Roll { get; set; }

        public bool IsVerified { get; set; }

        public DateTime UpdatedDate { get; set; }
        
        // Navigation property
        public Address? Address { get; set; } 
        public ICollection<OTP> OTP { get; set; }
    }
}
