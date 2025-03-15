using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LMS.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
           var data = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return data;
        }


        public async Task<User> GetUserByEmailForgotPassword(string email)
        {
            var data = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (data == null) throw new Exception("User Not found");
            return data;
        }

        public async Task<OTP> SaveOTP(OTP oTP)
        {
            await _context.OTPs.AddAsync(oTP);
            await _context.SaveChangesAsync();
            return oTP;
        }

        public async Task<OTP> CheckOTPExits(string otp)
        {
            var check = await _context.OTPs.FirstOrDefaultAsync(x => x.Code == otp);
            if (check == null) throw new Exception("OTP Not Found");

            return check;
        }

        public async Task RemoveOTP(string otp)
        {
            var sotp = await _context.OTPs.Include(u => u.User).FirstOrDefaultAsync(x => x.Code == otp);
            if (sotp == null) throw new Exception("OTP Not Found");
            _context.OTPs.Remove(sotp);
            await _context.SaveChangesAsync();
        }
        public async Task<User> ChangePassword(string email, string password)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (data == null) throw new Exception("User Not Found");
            data.Password = password;
            _context.Users.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task AddNewStudent(Student student) 
        {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();  
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync( d => d.Id == id);
        }

        public async Task DeleteUser(Guid id)
        { 
            var data = await GetUserById(id);
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task updateUserIsEmailConfirmed(Guid id)
        {
            var data = await GetUserById(id);
            data.IsEmailConfirmed = true;
            await _context.SaveChangesAsync();
        }

        public async Task<OTP> GetOtpByUserId(Guid id)
        { 
          var otp = await _context.OTPs.FirstOrDefaultAsync(d => d.UserId == id);
            return otp;
        }

        
    }
}
