using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByEmail(string email);
        Task<Student> GetStudentById(Guid id);
        Task VerifyRegister(Guid id);

        Task<ICollection<Student>> GetAllStudents();
    }
}
