using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> AddSubjectAsync(Subject subject);
        Task<Subject> GetSubjectByIdAsync(Guid id);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject> UpdateSubjectAsync(Subject subject);
        Task<bool> DeleteSubjectAsync(Guid id);
    }
}
