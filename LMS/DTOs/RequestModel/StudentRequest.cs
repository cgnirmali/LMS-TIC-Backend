using LMS.Assets.Enums;

namespace LMS.DTOs.RequestModel
{
    public class StudentRequest
    {
        
       
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UTEmail { get; set; }

        public string UTPassword { get; set; }

        public Status status { get; set; } 
        //public string Password { get; set; }
        //public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string UTNumber { get; set; }
        public string Gender { get; set; }
        //public string? ImageUrl { get; set; }
        //public bool AdminVerify { get; set; }

        public string? Address { get; set; }
        
    }
}
