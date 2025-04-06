using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface ILecturerRepository
    {
        Task AddLecturerUser(User user);
        Task AddLecturer(Lecturer lecturer);
        Task<List<Lecturer>> GetAllLecturer();
        Task<Lecturer> GetLecturerById(Guid id);
        Task UpdateLecturer(Lecturer lecturer);
        Task DeleteLecturer(Lecturer lecturer);
    }
}
