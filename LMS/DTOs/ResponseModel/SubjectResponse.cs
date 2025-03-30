namespace LMS.DTOs.ResponseModel
{
    public class SubjectResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
