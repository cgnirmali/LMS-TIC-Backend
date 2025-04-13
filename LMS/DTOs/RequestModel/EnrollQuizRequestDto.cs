namespace LMS.DTOs.RequestModel
{
    public class EnrollQuizRequestDto
    {
        public Guid StudentId { get; set; }
        public Guid QuizExamId { get; set; }
    }
}
