using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class Lecturer
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NIC { get; set; }

        [Required]
        public string? Address { get; set; }

        public string PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public Guid UserId { get; set; }

        // Navigation property
        public User User { get; set; }

        // Add IsDeleted property for soft deletes
        //public bool IsDeleted { get; set; } = false;
    }
}
