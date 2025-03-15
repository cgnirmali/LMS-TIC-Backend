using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LMS.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public StudentRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;


        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            var data = await _context.Students.SingleOrDefaultAsync(d => d.Email  == email);
            if (data == null) throw new Exception("Student Not found");
            return data;
        }

        public async Task<Student> GetStudentById(Guid id)
        {
            var data = await _context.Students.SingleOrDefaultAsync(d => d.Id == id);
            if (data == null) throw new Exception("Student Not found");
            return data;
        }

        public async Task VerifyRegister(Guid id)
        { 
        var data =await GetStudentById(id);
            data.AdminVerify = true;    
            await _context.SaveChangesAsync();

            var sameUser = await _context.Users.FirstOrDefaultAsync(x => x.Id ==data.UserId);
            sameUser.IsVerified = true;
            await _context.SaveChangesAsync();
        }
    }
}
