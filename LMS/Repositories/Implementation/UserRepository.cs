using LMS.DB;
using LMS.DB.Entities;

using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;


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
           var data = await _context.Users.SingleOrDefaultAsync(u => u.UTEmail == email);
            return data;
        }






        public async Task<OTP> GetLastOtpByEmail(string email)
        {
            return _context.OTPs
                .Where(o => o.UserEmail == email)
                .OrderByDescending(o => o.EndTime)
                .FirstOrDefault();
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

        public async Task RemoveOTP(Guid id)
        {
            var otp = await _context.OTPs.FirstOrDefaultAsync(x => x.Id == id);
            if (otp == null)
            {
                throw new Exception("OTP Not Found");
            }
         

            _context.OTPs.Remove(otp); 
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

        public async Task DeleteExpiredOtpsAsync()
        {
            var expiredOtps = await _context.OTPs
                .Where(x => x.EndTime < DateTime.UtcNow)
                .ToListAsync();

            if (expiredOtps.Any())
            {
                _context.OTPs.RemoveRange(expiredOtps);
                await _context.SaveChangesAsync();
            }
        }

   
        public async Task<OTP> UpdateOtpAsync(OTP otp)
        {
             _context.OTPs.Update(otp);
            await _context.SaveChangesAsync();
            return otp;
        }


        public async Task<User> ChangePassword(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<OTP> GetOtpByUserId(Guid id)
        { 
          var otp = await _context.OTPs.FirstOrDefaultAsync(d => d.UserId == id);
            return otp;
        }

        public async Task<OTP> GetOtpByEmailAsync(string email)
        {
            var otp = await _context.OTPs.FirstOrDefaultAsync(d => d.UserEmail == email);
            return otp;
        }




    }
}
