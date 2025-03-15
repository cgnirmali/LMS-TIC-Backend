using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IStaffRepository
    {
       



        Task AddStaffUser(User user);
             Task AddStaff(Staff staff);
    }
};
