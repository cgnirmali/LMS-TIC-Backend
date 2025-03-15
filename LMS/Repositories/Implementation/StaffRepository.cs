using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;

namespace LMS.Repositories.Implementation
{
    public class StaffRepository :IStaffRepository
    {


        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }

       


        public async Task AddStaffUser(User user)
        {
           
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddStaff(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
            ;
            await _context.SaveChangesAsync();
        }


    }
}
