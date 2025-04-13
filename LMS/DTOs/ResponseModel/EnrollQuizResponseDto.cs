namespace LMS.DTOs.ResponseModel
{
    public class EnrollQuizResponseDto
    {
        public string Message { get; set; }
        public Guid StudentAttemptId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
