namespace LMS.DB.Entities
{
    public class Staff
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
       
        public Guid UserId { get; set; }

        //navigation property
        public User User { get; set; }
       

    }
}
