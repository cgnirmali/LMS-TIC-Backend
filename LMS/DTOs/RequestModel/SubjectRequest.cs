namespace LMS.DTOs.RequestModel
{
    public class SubjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CourseId { get; set; } // Retained the property for CourseId
    }
}
