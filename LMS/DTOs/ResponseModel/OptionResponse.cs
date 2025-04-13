namespace LMS.DTOs.ResponseModel
{
    public class OptionResponse
    {

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Options { get; set; }
        public bool IsCorrect { get; set; }
    }
}
