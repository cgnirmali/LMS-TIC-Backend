namespace LMS.DB.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Assesment> Assesment { get; set; }
        public ICollection<QuizExam> QuizExam { get; set; }

        public ICollection<Questions> Questions { get; set; }

        public ICollection<Material> Materials { get; set; }    

    }
}
