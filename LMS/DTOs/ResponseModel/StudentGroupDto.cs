using LMS.Assets.Enums;

namespace LMS.DTOs.ResponseModel
{
    public class StudentGroupDto
    {

      
            public Guid StudentId { get; set; }
        public string UTNumber { get; set; }
        public Guid UserId { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UTEmail { get; set; }

        public string PhoneNumber { get; set; }
        public string GroupName { get; set; }

        public Status status { get; set; }
        public string Gender { get; set; }
    
        public string? Address { get; set; }



    }
}
