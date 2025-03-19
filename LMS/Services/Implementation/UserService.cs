
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

        public async Task<(TokenModel token, User user)> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }

           // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (user.Password != password)
                throw new Exception("Password Not Match");

            var role = user.role.ToString();

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

       

        public async Task<bool> CheckOTP(string otp)
        {
            var exits = await _userRepository.CheckOTPExits(otp);
            if (exits == null) throw new Exception("OTP not found");
            var today = DateTime.UtcNow;
            if (exits.EndTime < today) throw new Exception("OTP time out");
            await _userRepository.RemoveOTP(otp);
            return true;

        }

       

        public async Task<bool> ChangePassword(string email,string password)
        {
            var data = await _userRepository.ChangePassword(email,password);
            return data != null ? true : false;
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


        //public async Task<bool> VerifyOtpAsync(OtpVerifyDto otpVerifyDto)
        //{ 
        //    var record = await _userRepository.GetUserByEmailAsync(otpVerifyDto.Email);
        //    var otpRecord = await _userRepository.GetOtpByUserId(record.Id);
        //    if (otpRecord == null || otpRecord.Code != otpVerifyDto.Otp || otpRecord.OtpType != OtpType.Registration)
        //    {
        //        await _userRepository.DeleteUser(record.Id);
        //        throw new Exception("Invalid OTP");
        //    }
        //    else
        //    {
        //        await _userRepository.updateUserIsEmailConfirmed(record.Id);
        //        await _userRepository.RemoveOTP(otpVerifyDto.Otp);
        //        return true;
        //    }
        //}


        //public async Task<bool> SendOtpAsync(string email)
        //{
        //    string otp = GenerateOtp();
        //    var today = DateTime.Now;
        //    var expirationTime = DateTime.UtcNow.AddMinutes(7);

        //    // Check if the user already exists
        //    var user = await _userRepository.GetUserByEmailAsync(email);

        //    if (user == null)
        //    {
        //        // If user does not exist, create a new one
        //        user = new User
        //        {
        //            UTEmail = email,
        //            Id = Guid.NewGuid(),
        //            Password = null,
        //            IsVerified = false,
        //            role = Role.Student,
        //            CreatedDate = today
        //        };

        //        await _userRepository.AddUserAsync(user);
        //    }
        //    else
        //    {
        //        // If user exists, check for existing OTP
        //        var existingOtp = await _userRepository.GetOtpByEmailAsync(email);

        //        if (existingOtp != null && !user.IsEmailConfirmed)
        //        {
        //            // Remove old OTP if not verified
        //            await _userRepository.RemoveOTP(existingOtp.Code);

        //        }
        //    }

        //    // Generate new OTP and save it
        //    var otpEntity = new OTP
        //    {
        //        UserEmail = email,
        //        Code = otp,
        //        CreatedDate = today,
        //        EndTime = expirationTime,
        //        User = user,
        //        OtpType = OtpType.Registration
        //    };

        //    await _userRepository.SaveOTP(otpEntity);

        //    // Send email
        //    var mailRequest = new MailRequest
        //    {
        //        User = user,
        //        Type = EmailType.OTP,
        //        Otp = otp
        //    };

        //    await _sendMailService.SendEmail(mailRequest);

        //    return true;
        //}





        //public async Task<string> Register(RegisterRequest request)
        //{

        //    var user = await _userRepository.GetUserByEmailAsync(request.Email);
        //    //var existstudent = await _studentRepository.GetStudentByEmail(user.Email);
        //    if (request.Password == request.ConfirmPassword && user.IsEmailConfirmed == true)
        //    {
        //        var student = new Student
        //        {
        //            CreatedDate = DateTime.Now,
        //            UserId = user.Id,
        //            NIC = request.NIC,
        //            PhoneNumber = request.PhoneNumber,
        //            FirstName = request.FirstName,
        //            LastName = request.LastName,
        //            Email = request.Email,
        //            Gender = request.Gender,
        //            ImageUrl = request.ImageUrl,
        //            UTNumber = request.UTNumber,
        //            AdminVerify = true,
        //            Address = request.Address

        //        };

        //        await _userRepository.ChangePassword(user.Email, BCrypt.Net.BCrypt.HashPassword(request.Password));
        //        await _userRepository.AddNewStudent(student);
        //        await _context.SaveChangesAsync();

        //        return "Student Registered successfully";
        //    }
        //    return "Verify Your Email";

        //}

        //public async Task<Info> Register(RegisterRequest request)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(request.Email);

        //    if (user == null)
        //    {
        //        var info4 = new Info
        //        {
        //            text = "User No"
        //        };

        //        return info4;
        //    }

        //    if (!user.IsEmailConfirmed)
        //    {
        //        var info3 = new Info
        //        {
        //            text = "Please verify your email before registering as a student"
        //        };

        //        return info3;
        //    }

        //    var existstudent = await _studentRepository.GetStudentByEmail(user.Email);
        //    if (existstudent != null)
        //    {
        //        var info2 = new Info
        //        {
        //            text = "Student already exists"
        //        };

        //        return info2;
        //    }

        //    if (request.Password != request.ConfirmPassword)
        //    {
        //        var info1 = new Info
        //        {
        //            text = "Password and Confirm Password do not match"
        //        };

        //        return info1;
        //    }

        //    var student = new Student
        //    {
        //        CreatedDate = DateTime.Now,
        //        UserId = user.Id,
        //        NIC = request.NIC,
        //        PhoneNumber = request.PhoneNumber,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        Email = request.Email,
        //        Gender = request.Gender,
        //        ImageUrl = request.ImageUrl,
        //        UTNumber = request.UTNumber,
        //        AdminVerify = true,
        //        Address = request.Address
        //    };

        //    // Change Password
        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        //    await _userRepository.ChangePassword(user.Email, hashedPassword);

        //    // Add Student
        //    await _studentRepository.AddNewStudent(student);

        //    var info = new Info
        //    {
        //        text = "Student Registered successfully"
        //    };

        //    return info;
        //}







    }
}
