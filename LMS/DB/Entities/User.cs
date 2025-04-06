using LMS.Assets.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public string UTEmail { get; set; }

        public string? Password { get; set; }

        public Role role { get; set; }

        //public bool IsVerified { get; set; }
        //public bool IsEmailConfirmed { get; set; }



        // Navigation property

        public ICollection<OTP> OTP { get; set; }
    }
}
