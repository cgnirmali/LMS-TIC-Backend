using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {


        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public  async Task<User> getElementByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);

        }




    }
}
