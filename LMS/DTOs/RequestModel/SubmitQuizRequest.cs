namespace LMS.DTOs.RequestModel
{
    public class SubmitQuizRequest
    {
        public Guid StudentId { get; set; }
        public Guid StudentAttemptId { get; set; }
        public List<QuizAnswerDto> Answers { get; set; }
    }
    public class QuizAnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid SelectedOptionId { get; set; }
    }
}
