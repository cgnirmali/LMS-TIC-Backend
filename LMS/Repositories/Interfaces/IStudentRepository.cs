using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task AddNewStudent(Student student);
        Task<Student> GetStudentByEmail(string email);
        Task<Student?> GetStudentByIdAsync(Guid studentId);
        Task<Student> GetStudentById(Guid id);
        Task<bool> DeleteStudent(Guid studentId);
        //Task VerifyRegister(Guid id);
        Task<bool> UpdateStudentAsync(Guid studentId, Student updatedStudent, string? newPassword ,string?UTEmail);
        Task<ICollection<Student>> GetAllStudents();
    }
}
