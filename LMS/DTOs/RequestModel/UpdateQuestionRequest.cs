namespace LMS.DTOs.RequestModel
{
    public class UpdateQuestionRequest
    {
        public string Question { get; set; }
        public Guid SubjectId { get; set; }

        // Full replacement of options for simplicity
        public List<UpdateOptionRequest> Options { get; set; }
    }
}
