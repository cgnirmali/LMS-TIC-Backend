namespace LMS.DB.Entities
{
    public class QuizExam
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description {  get; set; }
        public DateTime Duration { get; set; }

        //navigation property
        public ICollection<Subject> Subjects { get; set; } 

    }
}
