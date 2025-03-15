
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

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
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

        public async Task<bool> VerifyOtpAsync(OtpVerifyDto otpVerifyDto)
        { 
            var record = await _userRepository.GetUserByEmailAsync(otpVerifyDto.Email);
            var otpRecord = await _userRepository.GetOtpByUserId(record.Id);
            if (otpRecord == null || otpRecord.Code != otpVerifyDto.Otp || otpRecord.OtpType != OtpType.Registration)
            {
                await _userRepository.DeleteUser(record.Id);
                throw new Exception("Invalid OTP");
            }
            else
            {
                await _userRepository.updateUserIsEmailConfirmed(record.Id);
                await _userRepository.RemoveOTP(otpVerifyDto.Otp);
                return true;
            }
        }

        public async Task<bool> ChangePassword(string email,string password)
        {
            var data = await _userRepository.ChangePassword(email,password);
            return data != null ? true : false;
        }

        public async Task<bool> SendOtpAsync(string email)
        {
            string otp = GenerateOtp();
            var today = DateTime.Now;
            var expirationTime = DateTime.UtcNow.AddMinutes(7);
           

            var user = new User();
            user.Email = email;
            user.Id = Guid.NewGuid();
            user.Password = null;
            user.IsVerified = false;
            user.role = Role.Student;
            user.CreatedDate = today;

            await _userRepository.AddUserAsync(user);

            var mailrequest = new MailRequest();
            mailrequest.User = user;
            mailrequest.Type = EmailType.OTP;
            mailrequest.Otp = otp;

            var OTP = new OTP
            {

                UserEmail = email,
                Code = otp,
                CreatedDate = today,
                EndTime = expirationTime,
                User = user,
                OtpType = OtpType.Registration


            };

            await _userRepository.SaveOTP(OTP);
            await _sendMailService.SendEmail(mailrequest);
            return true;
        }

        public TokenModel CreateToken(User user)
        {
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", user.Id.ToString()));
            claimsList.Add(new Claim("Email", user.Email));

            claimsList.Add(new Claim("Role", user.role.ToString()));


            var key = _configuration["JWT:Key"];
            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);


        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        

        public UserService (IUserRepository userRepository , IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }


        public async Task<string>  loginUser (string email, string password)
        {
            var data =await _userRepository.getElementByEmail(email);
            if (data == null) throw new Exception("User not found.");
            if (!BCrypt.Net.BCrypt.Verify(password, data.Password))
                throw new Exception("Incorrect password.");

            return CreateToken(data);


        }

        private string CreateToken(User data)
        {
            var claims = new List<Claim>
            {
                new Claim("EmailUser", data.Email),
                new Claim("Role", data.Roll.ToString())
            };

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

        public async Task<string> Register(RegisterRequest request)
        {

            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            //var existstudent = await _studentRepository.GetStudentByEmail(user.Email);
            if (request.Password == request.ConfirmPassword && user.IsEmailConfirmed == true)
            {
                var student = new Student
                {
                    CreatedDate = DateTime.Now,
                    UserId = user.Id,
                    NIC = request.NIC,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender,
                    ImageUrl = request.ImageUrl,
                    UTNumber = request.UTNumber,
                    AdminVerify = true,
                    Address = request.Address

                };

                await _userRepository.ChangePassword(user.Email, BCrypt.Net.BCrypt.HashPassword(request.Password));
                await _userRepository.AddNewStudent(student);
                await _context.SaveChangesAsync();

                return "Student Registered successfully";
            }
            return "Verify Your Email";

        }

        //public async Task<string> Register(RegisterRequest request)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(request.Email);

        //    if (user == null)
        //    {
        //        return "User not found";
        //    }

        //    var existstudent = await _studentRepository.GetStudentByEmail(user.Email);

        //    if (existstudent != null)
        //    {
        //        return "Student already exists";
        //    }

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
      claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }
}
