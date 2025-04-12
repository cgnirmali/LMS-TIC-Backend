using LMS.Assets.Enums;
using LMS.DB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DTOs.RequestModel
{
    public class UpdatedStudentDto
    {


     
        public string UTNumber { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string? Address { get; set; }

        public Guid GroupId { get; set; }

       

      
    }
}
