namespace LMS.DTOs.ResponseModel
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Question { get; set; }
        public Guid SubjectId { get; set; }

        public List<OptionResponse> Options { get; set; }
    }
}
