using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task AddAsync(Group group)
        {
            group.Id = Guid.NewGuid();
            group.CreatedDate = DateTime.UtcNow;
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}
