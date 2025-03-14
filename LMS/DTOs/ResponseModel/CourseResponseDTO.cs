namespace LMS.DTOs.ResponseModel
{
    public class CourseResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public Guid BatchId { get; set; }
        public BatchResponseDTO Batch { get; set; }
    }
}
