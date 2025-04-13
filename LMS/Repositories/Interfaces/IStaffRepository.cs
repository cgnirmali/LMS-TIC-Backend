using LMS.DB.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Task AddStaffUser(User user);
        Task AddStaff(Staff staff);
        Task<List<Staff>> GetAllStaff();
        Task<Staff> GetStaffById(Guid id);
        Task UpdateStaff(Staff staff);
        Task DeleteStaff(Staff staff);

        Task<Student> GetStaffByEmail(string email);
    }
}
