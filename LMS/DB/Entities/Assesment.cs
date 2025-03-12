namespace LMS.DB.Entities
{
    public class Assesment
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }
        public DateTime DueTime { get; set; }
       
        public string URL { get; set; }
        public Guid SubjectId {  get; set; }

        //navigate
        public Subject Subject { get; set; }

    }
}
