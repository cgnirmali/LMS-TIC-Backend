using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly EmailService _sendMailService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, EmailService sendMailService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _sendMailService = sendMailService;
            _configuration = configuration;
        }

        //public async Task<(string Token, User user)> Authenticate(string email, string password)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(email);
        //    if (user == null)
        //    {
        //        throw new Exception("User Not Found");
        //    }

        //    var role = user.roll ?? throw new Exception("User role not found");

        //    var token = _tokenRepository.GenerateToken(user);

        //    return (token, user);
        //}


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

        private TokenModel CreateToken(User user)
        {
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", user.Id.ToString()));
            claimsList.Add(new Claim("Email", user.Email));

            claimsList.Add(new Claim("Role", user.role.ToString()));


            var key = _configuration["JWT:Key"];
            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

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

    }
}
