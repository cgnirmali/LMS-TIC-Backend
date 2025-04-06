namespace LMS.DB.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UTNumber { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public bool AdminVerify { get; set; }
        public string? Address { get; set; }


        //navigation property
        public User User { get; set; }
    
        public ICollection<AssesmentSubmission> Assesmentsubmission { get; set; }
        public ICollection<Attendance> Attendance { get; set; }

    }
}
