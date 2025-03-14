using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LMS.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
           _courseRepository = courseRepository;
        }

        public async Task<CourseResponseDTO> GetCourseByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return new CourseResponseDTO
            {
                Id = course.Id,
                CreatedDate = course.CreatedDate,
                Name = course.Name,
                BatchId = course.BatchId,
                Batch = new BatchResponseDTO
                {
                    Id = course.Batch.Id,
                    CreatedDate = course.Batch.CreatedDate,
                    Name = course.Batch.Name
                }
            };
        }

        public async Task<IEnumerable<CourseResponseDTO>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(course => new CourseResponseDTO
            {
                Id = course.Id,
                CreatedDate = course.CreatedDate,
                Name = course.Name,
                BatchId = course.BatchId,
                Batch = new BatchResponseDTO
                {
                    Id = course.Batch.Id,
                    CreatedDate = course.Batch.CreatedDate,
                    Name = course.Batch.Name
                }
            });
        }

        public async Task<CourseResponseDTO> CreateCourseAsync(CourseRequerstDTO request)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                BatchId = request.BatchId
            };

            await _courseRepository.AddAsync(course);

            return new CourseResponseDTO
            {
                Id = course.Id,
                CreatedDate = course.CreatedDate,
                Name = course.Name,
                BatchId = course.BatchId
            };
        }

        public async Task UpdateCourseAsync(Guid id, CourseRequerstDTO request)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                course.Name = request.Name;
                course.BatchId = request.BatchId;
                await _courseRepository.UpdateAsync(course);
            }
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
        }
    }

    
}
