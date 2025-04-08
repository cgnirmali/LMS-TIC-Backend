using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectResponse> CreateSubjectAsync(SubjectRequest request)
        {
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                Description = request.Description,
                CourseId = request.CourseId

            };

            var createdSubject = await _subjectRepository.AddSubjectAsync(subject);

            return new SubjectResponse
            {
                Id = createdSubject.Id,
                CreatedDate = createdSubject.CreatedDate,
                Name = createdSubject.Name,
                Description = createdSubject.Description,
                CourseId = createdSubject.CourseId  
            };
        }

        public async Task<List<SubjectResponse>> GetSubjectByCourseIdAsync(Guid courseId)
        {
            var subjects = await _subjectRepository.GetSubjectByCourseIdAsync(courseId);
            if (subjects == null || !subjects.Any()) return new List<SubjectResponse>();

            return subjects.Select(subject => new SubjectResponse
            {
                Id = subject.Id,
                CreatedDate = subject.CreatedDate,
                Name = subject.Name,
                Description = subject.Description,
                CourseId = subject.CourseId
            }).ToList();
        }

        public async Task<SubjectResponse> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subject == null) return null;

            return new SubjectResponse
            {
                Id = subject.Id,
                CreatedDate = subject.CreatedDate,
                Name = subject.Name,
                Description = subject.Description,
                CourseId = subject.CourseId
            };
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllSubjectsAsync();
            return subjects.Select(s => new SubjectResponse
            {
                Id = s.Id,
                CreatedDate = s.CreatedDate,
                Name = s.Name,
                Description = s.Description,
                CourseId = s.CourseId
            });
        }

        public async Task<SubjectResponse> UpdateSubjectAsync(Guid id, SubjectRequest request)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subject == null) return null;

            subject.Name = request.Name;
            subject.Description = request.Description;

            var updatedSubject = await _subjectRepository.UpdateSubjectAsync(subject);

            return new SubjectResponse
            {
                Id = updatedSubject.Id,
                CreatedDate = updatedSubject.CreatedDate,
                Name = updatedSubject.Name,
                Description = updatedSubject.Description,
                CourseId= updatedSubject.CourseId
            };
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            return await _subjectRepository.DeleteSubjectAsync(id);
        }
    }
}
