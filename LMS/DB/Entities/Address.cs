namespace LMS.DB.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string Lane {  get; set; }
        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }
        public Guid UserId { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
