namespace LMS.DTOs.RequestModel
{
    public class UpdateOptionRequest
    {
        public Guid Id { get; set; } // Needed to track existing options
        public string Options { get; set; }
        public bool IsCorrect { get; set; }
    }
}
