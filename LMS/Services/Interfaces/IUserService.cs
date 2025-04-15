
﻿using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<(TokenModel token, User user)> login(string email, string password)
            ;
        Task RemoveExpiredOtpsAsync();

        Task<bool> VerifyOtpAsync(OtpVerifyDto otpVerifyDto);
     
        Task<bool> ChangePassword(string email, string password);
        Task<bool> SendOtpAsync(string email);
        string GenerateOtp();
      
        TokenModel CreateToken(User user);
        

        

    }
}
