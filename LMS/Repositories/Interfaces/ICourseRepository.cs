using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetAllAsync();

        Task AddAsync(Course course);

        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id);
    }
}
