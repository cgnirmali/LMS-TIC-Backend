using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ICollection<Student>> GetAllStudents();
        Task<Student> GetStudentByEmail(string email);
        Task<Info> AddNewStudent(StudentRequest studentRequest);
        Task<Student?> GetStudentByIdAsync(Guid studentId);
        Task<bool> UpdateStudentAsync(Guid studentId, Student updatedStudent, string? newPassword, string? UTEmail);
        Task<bool> DeleteStudent(Guid id);
    }
}
