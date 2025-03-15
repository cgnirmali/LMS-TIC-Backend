using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
                BatchId = course.BatchId,  // BatchId is already part of the Course entity
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
                BatchId = request.BatchId  // This should link the course to a batch
            };

            await _courseRepository.AddAsync(course); // Adding the course

            // Now, load the course with the batch to include the Batch data in the response
            var courseWithBatch = await _courseRepository.GetByIdAsync(course.Id);

            return new CourseResponseDTO
            {
                Id = courseWithBatch.Id,
                CreatedDate = courseWithBatch.CreatedDate,
                Name = courseWithBatch.Name,
                BatchId = courseWithBatch.BatchId,
                Batch = new BatchResponseDTO
                {
                    Id = courseWithBatch.Batch.Id,
                    CreatedDate = courseWithBatch.Batch.CreatedDate,
                    Name = courseWithBatch.Batch.Name
                }
            };
        }



        public async Task UpdateCourseAsync(Guid id, string name )
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                course.Name = name;
                
                await _courseRepository.UpdateAsync(course);
            }
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
        }
    }

    
}
