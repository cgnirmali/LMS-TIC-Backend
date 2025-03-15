namespace LMS.DB.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }

        public Guid BatchId {  get; set; }
        
        // Navigation property
        public Batch Batch { get; set; }
        //public ICollection<Group> Group {  get; set; }
        //public ICollection<Subject> Subject { get; set; }
        

    }
}
