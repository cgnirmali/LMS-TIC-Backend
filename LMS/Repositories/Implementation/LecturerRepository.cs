using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Repositories.Implementation
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly AppDbContext _context;

        public LecturerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLecturerUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddLecturer(Lecturer lecturer)
        {
            await _context.Lecturers.AddAsync(lecturer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Lecturer>> GetAllLecturer()
        {
            return await _context.Lecturers.Include(l => l.User).ToListAsync();
        }

        public async Task<Lecturer> GetLecturerById(Guid id)
        {
            return await _context.Lecturers.Include(l => l.User).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task UpdateLecturer(Lecturer lecturer)
        {
            _context.Lecturers.Update(lecturer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLecturer(Lecturer lecturer)
        {
            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
        }
    }
}
