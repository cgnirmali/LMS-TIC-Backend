using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Services.Implementation
{
    public class StaffService : IStaffService
    {

        private readonly IStaffRepository _staffRepository;
        private readonly EmailService _emailService;
        //private readonly ILogger<StaffService> c_logger;

        private readonly IConfiguration _configuration;

        public StaffService(IStaffRepository staffRepository , IConfiguration configuration , EmailService emailService)
        {
            _staffRepository = staffRepository;
            _emailService = emailService;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<string> AddStaff(StaffRequest staffRequest , UserStaff_LectureRequest userStaff_LectureRequest)
        {
            try
            {

                if (string.IsNullOrEmpty(userStaff_LectureRequest.Email))
                {
                    throw new ArgumentException("Email cannot be null or empty");
                }

                string randomString = GenerateRandomString(6);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomString);

            


                var user = new User
                {
                    Id = Guid.NewGuid(),

                    Email = userStaff_LectureRequest.Email,
                    Password = hashedPassword,
                    Roll = Assets.Enums.Roll.Staff,
                    CreatedDate = DateTime.UtcNow
                };



                await _staffRepository.AddStaffUser(user);

                await _emailService.SendEmailAsync(
                user.Email,
               "Your Account Credentials",
               $"Your password: {randomString}"
                );


                var staff = new Staff
                {
                    Id = Guid.NewGuid(),
                    Name = staffRequest.Name,
                    PhoneNumber = staffRequest.PhoneNumber,
                    ImageUrl = null,
                    NIC = staffRequest.NIC,
                    Address = staffRequest.Address,
                    UserId = user.Id,
                    CreatedDate = DateTime.UtcNow
                };


                await _staffRepository.AddStaff(staff);
                    return CreateToken(user);


            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error adding staff: {ex.Message}");
                throw new Exception($"Error while adding staff: {ex.Message}", ex);
            }
        }






         public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("EmailUser", user.Email),
                new Claim("Role", user.Roll.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
