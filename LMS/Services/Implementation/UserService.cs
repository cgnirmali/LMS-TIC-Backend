
﻿using LMS.Assets.Enums;
using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Numerics;


namespace LMS.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly EmailService _sendMailService;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public UserService(IUserRepository userRepository, EmailService sendMailService, IConfiguration configuration, AppDbContext context, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _sendMailService = sendMailService;
            _configuration = configuration;
            _context = context;
            _studentRepository = studentRepository;
        }

        public async Task<(TokenModel token, User user)> login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
      

            if (user == null) throw new Exception("User not found.");
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Incorrect password.");


            var token = CreateToken(user);

            return (token, user);
        }



      


        public string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            return (BitConverter.ToUInt32(bytes, 0) % 1000000).ToString("D6");
        }

      

        public TokenModel CreateToken(User user)
        {
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", user.Id.ToString()));
            claimsList.Add(new Claim("Email", user.UTEmail));  
            claimsList.Add(new Claim("Role", user.role.ToString()));





            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],

                claims: claimsList,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
                );
            var response = new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return response;
        }


        public async Task<bool> VerifyOtpAsync(OtpVerifyDto otpVerifyDto)
        {
            var record = await _userRepository.GetLastOtpByEmail(otpVerifyDto.Email);


            if (record==null)
            {
                throw new Exception("email not found");
            }
            
            if (record.Code == otpVerifyDto.Otp)
            {

                record.OtpType = OtpType.PasswordResetConfirm;
                await _userRepository.UpdateOtpAsync(record);
                return true;
            }
            else
            {
                throw new Exception("OTP not found");
            }
        }


        public async Task<bool> SendOtpAsync(string email)
        {
      
            string otp = GenerateOtp();
            var today = DateTime.Now;
            var expirationTime = DateTime.UtcNow.AddMinutes(7);

            // Check if the user already exists
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");

            }
            else
            {

                // Generate new OTP and save it
                var otpEntity = new OTP
                {
                    Id = new Guid(),
                    UserEmail = user.UTEmail,
                    Code = otp,
                    CreatedDate = today,
                    EndTime = expirationTime,
                    UserId = user.Id,
                    OtpType = OtpType.PasswordReset
                };

                await _userRepository.SaveOTP(otpEntity);

                // Send email
                var updatemailRequest = new UpdateMailRequest
                {
                    UTEmail = user.UTEmail,
                    Otp = otp
                };

                await _sendMailService.SendEmailforUpdate(updatemailRequest);

                return true;


            }
        }


        public async Task<bool> ChangePassword(string email, string password)
        {
            var record = await _userRepository.GetLastOtpByEmail(email);

            if ((int)record.OtpType == 1)
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null)
                    throw new Exception("User not found");

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                user.Password = hashedPassword;

                await _userRepository.ChangePassword(user);

                await _userRepository.RemoveOTP(record.Id);
                return true; 
            }

            return false; 
        }



        public async Task RemoveExpiredOtpsAsync()
        {
            await _userRepository.DeleteExpiredOtpsAsync();
        }







    }
}
