namespace LMS.DB.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssessmentId { get; set; }  // Corrected property name

        // Navigation properties
        public ICollection<Course> Course { get; set; }
        public ICollection<Assesment> Assessment { get; set; }  // Corrected the spelling here too
        public ICollection<QuizExam> QuizExam { get; set; }
    }
}
