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
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public StaffService(IStaffRepository staffRepository, IConfiguration configuration, EmailService emailService)
        {
            _staffRepository = staffRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<string> AddStaff(StaffRequest staffRequest)
        {
            if (string.IsNullOrWhiteSpace(staffRequest.Address))
                throw new ArgumentException("Address cannot be null or empty");

           

            string randomPassword = GenerateRandomString(6);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);

            var user = new User
            {
                Id = Guid.NewGuid(),
                UTEmail = staffRequest.UTEmail,
                Password = hashedPassword,
                role = Assets.Enums.Role.Staff,
                CreatedDate = DateTime.UtcNow
            };

            await _staffRepository.AddStaffUser(user);
            await _emailService.SendEmailtoLoginAsync(staffRequest.UserEmail, "Your Account Credentials", $"Your password: {randomPassword} Your UTEmail: {staffRequest.UTEmail} Your  UTEmail password: {staffRequest.UTPassword}");

            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                UserEmail = staffRequest.UserEmail,
                Name = staffRequest.Name,
                PhoneNumber = staffRequest.PhoneNumber,
                NIC = staffRequest.NIC,
                Address = staffRequest.Address,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow
            };

            await _staffRepository.AddStaff(staff);
            return CreateToken(user);
        }

        public async Task<List<StaffResponse>> GetAllStaff()
        {
            var staffs = await _staffRepository.GetAllStaff();
            return staffs.Select(s => new StaffResponse
            {
                Id = s.Id,
                Name = s.Name,
                UTEmail = s.User.UTEmail,
                UserEmail = s.UserEmail,
                PhoneNumber = s.PhoneNumber,
                NIC = s.NIC,
                Address = s.Address
            }).ToList();
        }

        public async Task<StaffResponse> GetStaffById(Guid id)
        {
            var staff = await _staffRepository.GetStaffById(id);
            if (staff == null) throw new Exception("Staff not found");

            return new StaffResponse
            {
                Id = staff.Id,
                Name = staff.Name,
                UTEmail = staff.User.UTEmail,
                UserEmail = staff.UserEmail,
                PhoneNumber = staff.PhoneNumber,
                NIC = staff.NIC,
                Address = staff.Address
            };
        }

        public async Task UpdateStaff(Guid id, UpdateStaffRequest staffRequest)
        {
            var staff = await _staffRepository.GetStaffById(id);
            if (staff == null) throw new Exception("Staff not found");

            staff.Name = staffRequest.Name;
            staff.PhoneNumber = staffRequest.PhoneNumber;
            staff.NIC = staffRequest.NIC;
            staff.Address = staffRequest.Address;
            staff.UserEmail = staffRequest.UserEmail;

            await _staffRepository.UpdateStaff(staff);
        }

        public async Task DeleteStaff(Guid id)
        {
            var staff = await _staffRepository.GetStaffById(id);
            if (staff == null) throw new Exception("Staff not found");

            await _staffRepository.DeleteStaff(staff);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("EmailUser", user.UTEmail),
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
