namespace LMS.DTOs.ResponseModel
{
    public class StaffResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }

     
        public string UTEmail { get; set; }

        public string UTPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }




    }
}
