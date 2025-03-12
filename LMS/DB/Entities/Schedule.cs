namespace LMS.DB.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }

        public Guid LecturerId { get; set; }

        //navigation property
        public Lecturer Lecturer { get; set; }
    }
}
