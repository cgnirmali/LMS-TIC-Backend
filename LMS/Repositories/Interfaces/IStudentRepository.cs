using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task AddNewStudent(Student student);
        Task<Student> GetStudentByEmail(string email);
        Task<StudentGroupDto> GetStudentByIdAsync(Guid studentId);
     
        Task<bool> DeleteStudent(Guid studentId);
        //Task VerifyRegister(Guid id);
        Task<bool> UpdateStudentAsync(Guid studentId, UpdatedStudentDto updatedStudent);
        Task<List<StudentGroupDto>> GetAllStudents();
    }
}
