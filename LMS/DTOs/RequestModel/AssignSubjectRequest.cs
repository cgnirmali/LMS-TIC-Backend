namespace LMS.DTOs.RequestModel
{
    public class AssignSubjectRequest
    {
        public Guid ExamId { get; set; }
        public List<Guid> SubjectIds { get; set; }
    }
}
