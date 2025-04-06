using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Implementation
{
    public class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public LecturerService(ILecturerRepository lecturerRepository, IConfiguration configuration, EmailService emailService)
        {
            _lecturerRepository = lecturerRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<string> AddLecturer(LecturerRequest lecturerRequest, UserStaff_LectureRequest userStaff_LectureRequest)
        {
            if (string.IsNullOrWhiteSpace(lecturerRequest.Address))
                throw new ArgumentException("Address cannot be null or empty");

            if (string.IsNullOrEmpty(userStaff_LectureRequest.Email))
                throw new ArgumentException("Email cannot be null or empty");

            string randomPassword = GenerateRandomString(6);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = userStaff_LectureRequest.Email,
                Password = hashedPassword,
                role = Assets.Enums.Role.lectures,
                CreatedDate = DateTime.UtcNow
            };

            await _lecturerRepository.AddLecturerUser(user);
            await _emailService.SendEmailtoLoginAsync(user.Email, "Your Account Credentials", $"Your password: {randomPassword}");

            var lecturer = new Lecturer
            {
                Id = Guid.NewGuid(),
                Name = lecturerRequest.Name,
                PhoneNumber = lecturerRequest.PhoneNumber,
                NIC = lecturerRequest.NIC,
                Address = lecturerRequest.Address,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow
            };

            await _lecturerRepository.AddLecturer(lecturer);
            return CreateToken(user);
        }

        public async Task<List<LecturerResponse>> GetAllLecturer()
        {
            var lecturers = await _lecturerRepository.GetAllLecturer();
            return lecturers.Select(l => new LecturerResponse
            {
                Id = l.Id,
                Name = l.Name,
                Email = l.User.Email,
                PhoneNumber = l.PhoneNumber,
                NIC = l.NIC,
                Address = l.Address
            }).ToList();
        }

        public async Task<LecturerResponse> GetLecturerById(Guid id)
        {
            var lecturer = await _lecturerRepository.GetLecturerById(id);
            if (lecturer == null) throw new Exception("Lecturer not found");

            return new LecturerResponse
            {
                Id = lecturer.Id,
                Name = lecturer.Name,
                Email = lecturer.User.Email,
                PhoneNumber = lecturer.PhoneNumber,
                NIC = lecturer.NIC,
                Address = lecturer.Address
            };
        }

        public async Task UpdateLecturer(Guid id, LecturerRequest lecturerRequest)
        {
            var lecturer = await _lecturerRepository.GetLecturerById(id);
            if (lecturer == null) throw new Exception("Lecturer not found");

            lecturer.Name = lecturerRequest.Name;
            lecturer.PhoneNumber = lecturerRequest.PhoneNumber;
            lecturer.NIC = lecturerRequest.NIC;
            lecturer.Address = lecturerRequest.Address;

            await _lecturerRepository.UpdateLecturer(lecturer);
        }

        public async Task DeleteLecturer(Guid id)
        {
            var lecturer = await _lecturerRepository.GetLecturerById(id);
            if (lecturer == null) throw new Exception("Lecturer not found");

            await _lecturerRepository.DeleteLecturer(lecturer);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("EmailUser", user.Email),
                new Claim("Role", user.role.ToString())
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

        private static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
