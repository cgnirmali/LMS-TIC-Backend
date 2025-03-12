namespace LMS.DB.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }

        public Guid CourseId { get; set; }

        //navigation property
        public  Course Course{ get; set; }
    }
}
