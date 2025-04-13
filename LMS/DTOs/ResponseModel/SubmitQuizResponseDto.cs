namespace LMS.DTOs.ResponseModel
{
    public class SubmitQuizResponseDto
    {
        public string Message { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public string ScoreDisplay { get; set; } // "8 out of 10"
    }
}
