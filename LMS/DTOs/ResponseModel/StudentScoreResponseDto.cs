namespace LMS.DTOs.ResponseModel
{
    public class StudentScoreResponseDto
    {
        public Guid StudentId { get; set; }
        public Guid QuizExamId { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
