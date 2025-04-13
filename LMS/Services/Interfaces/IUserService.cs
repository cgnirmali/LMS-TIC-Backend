
﻿using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<(TokenModel token, User user)> login(string email, string password)
            ;

        Task<bool> CheckOTP(string otp);
        Task<bool> ChangePassword(string email, string password);
        //Task<bool> SendOtpAsync(string email);
        string GenerateOtp();
        //Task<Info> Register(RegisterRequest request);
        //Task<bool> VerifyOtpAsync(OtpVerifyDto otpVerifyDto);
        TokenModel CreateToken(User user);
         //Task<string> loginUser(string email, string password);

        

    }
}
