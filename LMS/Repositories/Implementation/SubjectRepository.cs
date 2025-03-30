using LMS.DB.Entities;
using LMS.DB;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class SubjectRepository : ISubjectRepository
    {


        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subject> AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<List<Subject>> GetSubjectByCourseIdAsync(Guid id)
        {

            return await _context.Subjects.Where(s => s.CourseId == id).ToListAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(Guid id)
        {

            var data = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            return data;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            var subject = await GetSubjectByIdAsync(id);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}



