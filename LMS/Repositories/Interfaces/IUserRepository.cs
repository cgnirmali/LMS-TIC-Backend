using LMS.DB.Entities;
using LMS.DTOs.RequestModel;

namespace LMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailForgotPassword(string email);
        Task<OTP> SaveOTP(OTP oTP);
        Task<OTP> CheckOTPExits(string otp);
        Task<User> ChangePassword(string email, string password);
        Task RemoveOTP(string otp);
    }
}
