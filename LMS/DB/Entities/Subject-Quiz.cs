namespace LMS.DB.Entities
{
    public class Subject_Quiz
    {
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Guid QuizExamId {  get; set; }
        public QuizExam QuizExam { get; set; }
    }
}
