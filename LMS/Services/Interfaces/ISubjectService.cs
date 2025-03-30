using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface ISubjectService
    {

        Task<SubjectResponse> CreateSubjectAsync(SubjectRequest request);
        Task<List<SubjectResponse>> GetSubjectByCourseIdAsync(Guid CourseId);
        Task<IEnumerable<SubjectResponse>> GetAllSubjectsAsync();
        Task<SubjectResponse> UpdateSubjectAsync(Guid id, SubjectRequest request);
        Task<bool> DeleteSubjectAsync(Guid id);
    }
}
