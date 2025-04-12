using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class Staff
    {
        [Key]
        public Guid Id { get; set; }


        public DateTime CreatedDate { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }

        public string UserEmail { get; set; }


        [Required]
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public Guid UserId { get; set; }

        //navigation property
        public User User { get; set; }


    }
}
