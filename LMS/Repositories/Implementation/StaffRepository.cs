using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Repositories.Implementation
{
    public class StaffRepository : IStaffRepository
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
            await _context.SaveChangesAsync();
        }

        public async Task<List<Staff>> GetAllStaff()
        {
            return await _context.Staffs.Include(s => s.User).ToListAsync();
        }

        public async Task<Staff> GetStaffById(Guid id)
        {
            return await _context.Staffs.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateStaff(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStaff(Staff staff)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }
}
