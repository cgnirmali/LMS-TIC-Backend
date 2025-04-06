using LMS.Assets.Enums;

namespace LMS.DTOs.RequestModel
{
    public class UpdateStudentDto
    {
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UTEmail { get; set; }

        public string UTPassword { get; set; }

        public Status status { get; set; }
       
        public string PhoneNumber { get; set; }

        public string UTNumber { get; set; }
        public string Gender { get; set; }

        public string? Address { get; set; }
        public string? NewPassword { get; set; } // Optional Password Update
    }
}
