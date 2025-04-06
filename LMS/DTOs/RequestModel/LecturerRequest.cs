using System.ComponentModel.DataAnnotations;

namespace LMS.DTOs.RequestModel
{
    public class LecturerRequest
    {
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }

        [Required]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


    }
}
