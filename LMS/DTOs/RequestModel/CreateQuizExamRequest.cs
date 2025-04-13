namespace LMS.DTOs.RequestModel
{
    public class CreateQuizExamRequest
    {
        public string ExamName { get; set; }
        public TimeSpan DurationInHours { get; set; }
        public string Description { get; set; }
    }
}
