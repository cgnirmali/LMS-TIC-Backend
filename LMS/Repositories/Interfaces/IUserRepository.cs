using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> getElementByEmail(string email);
    }
}
