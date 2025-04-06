namespace LMS.DTOs.RequestModel
{
    public class RegisterRequest
    {
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UTNumber { get; set; }
        public string Gender { get; set; }
        public string? ImageUrl { get; set; }
        public bool AdminVerify { get; set; }

        public string? Address { get; set; }
    }
}
