namespace LMS.DTOs.RequestModel
{
    public class CreateOptionRequest
    {
        public Guid QuestionId { get; set; }  // Added questionId
        public List<AddOptionRequest> Options { get; set; }  // List of options to be added
    }
}
