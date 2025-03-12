namespace LMS.DB.Entities
{
    public class Lecturer
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl  { get; set; }
        public string NIC { get; set; }
    }
}
