namespace LMS.DTOs.RequestModel
{
    public class AssignQuestionsRequest
    {
        public Guid SubjectQuizId { get; set; }
        public List<Guid> QuestionIds { get; set; }
    }
}
