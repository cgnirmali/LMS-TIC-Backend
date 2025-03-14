namespace LMS.DTOs.ResponseModel
{
    public class BatchResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public ICollection<CourseResponseDTO> Courses { get; set; }
    }
}

