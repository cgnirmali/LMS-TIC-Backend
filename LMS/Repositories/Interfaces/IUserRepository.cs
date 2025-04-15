using LMS.DB.Entities;

using LMS.DTOs.RequestModel;

namespace LMS.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        //Task<User> GetUserByEmailForgotPassword(string email);
        Task<OTP> SaveOTP(OTP oTP);

        Task DeleteExpiredOtpsAsync();
        Task<OTP> UpdateOtpAsync(OTP otp);
        Task<OTP> CheckOTPExits(string otp);
        Task<User> ChangePassword(User user);
        Task<OTP> GetLastOtpByEmail(string email);
        Task RemoveOTP(Guid id);

        Task<User> GetUserById(Guid id);
        Task<OTP> GetOtpByUserId(Guid id);
        //Task updateUserIsEmailConfirmed(Guid id);
        Task DeleteUser(Guid id);
        //Task<User> getElementByEmail(string email);
        Task<OTP> GetOtpByEmailAsync(string email);

    }
}
