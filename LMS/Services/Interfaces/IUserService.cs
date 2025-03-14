using LMS.DB.Entities;
using LMS.DTOs.RequestModel;

namespace LMS.Services.Interfaces
{
    public interface IUserService
    {
        //Task<(string Token, User user)> Authenticate(string email, string password);
        
        Task<bool> CheckOTP(string otp);
        Task<bool> ChangePassword(string email, string password);
        Task<bool> SendOtpAsync(string email);
        string GenerateOtp();
    }
}
