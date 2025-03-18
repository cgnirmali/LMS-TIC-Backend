using LMS.DB.Entities;

namespace LMS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ICollection<Student>> GetAllStudents();
    }
}
