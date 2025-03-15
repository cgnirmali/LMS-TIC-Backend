using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Services.Implementation
{
    public class UserService : IUserService
    {

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
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }
}
