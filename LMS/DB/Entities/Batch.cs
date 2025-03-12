namespace LMS.DB.Entities
{
    public class Batch
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }

        //navigation property
        public ICollection<Course> Course { get; set; }

    }
}
