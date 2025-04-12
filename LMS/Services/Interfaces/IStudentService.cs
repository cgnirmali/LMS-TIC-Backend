using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentGroupDto>> GetAllStudents();
        Task<Student> GetStudentByEmail(string email);
        Task<Info> AddNewStudent(StudentRequest studentRequest);
        Task<StudentGroupDto?> GetStudentByIdAsync(Guid studentId);
        Task<bool> UpdateStudentAsync(Guid studentId, UpdatedStudentDto updatedStudent);
        Task<bool> DeleteStudent(Guid id);
    }
}
