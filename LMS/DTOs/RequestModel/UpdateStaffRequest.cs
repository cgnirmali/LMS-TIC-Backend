using System.ComponentModel.DataAnnotations;

namespace LMS.DTOs.RequestModel
{
    public class UpdateStaffRequest
    {

        public string Name { get; set; }
   
        public string NIC { get; set; }

        public string UserEmail { get; set; }
      
     
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
