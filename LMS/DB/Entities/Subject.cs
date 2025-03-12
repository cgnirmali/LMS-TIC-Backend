namespace LMS.DB.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssesmentId { get; set; }

        //navigation property
        public ICollection<Course> Course { get; set; }
        public ICollection<Assesment> Assesment { get; set; }
    }
}
