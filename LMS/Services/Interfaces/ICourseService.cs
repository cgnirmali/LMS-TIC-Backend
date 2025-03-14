using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface ICourseService
    {
        Task<CourseResponseDTO> GetCourseByIdAsync(Guid id);
        Task<IEnumerable<CourseResponseDTO>> GetAllCoursesAsync();

        Task<CourseResponseDTO> CreateCourseAsync(CourseRequerstDTO request);

        Task UpdateCourseAsync(Guid id, CourseRequerstDTO request);

        Task DeleteCourseAsync(Guid id);
    }
        
}
