//using LMS.Assets.Enums;
//using LMS.DB.Entities;
//using LMS.DTOs.RequestModel;
//using LMS.Services.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LMS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IUserService _userService;
        

//        public AuthController(IUserService userService)
//        {
//            _userService = userService;
           
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login( LoginRequest loginRequest)
//        {
//            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
//            {
//                return BadRequest("Email and Password are required.");
//            }

//            try
//            {
//                const string adminEmail = "admin@gmail.com";
//                const string adminPassword = "abi@123";

//                if (loginRequest.Email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) &&
//                    loginRequest.Password == adminPassword)
//                {
//                    var adminId = Guid.NewGuid();
//                    var adminToken = _userService.CreateToken(new User
//                    {
//                        UTEmail = adminEmail,
//                        Id = adminId,
//                        role = Role.Admin,
//                    });

//                    return Ok(new
//                    {
//                        Token = adminToken,
//                        User = new
//                        {
//                            UTEmail = adminEmail,
//                            Role = Role.Admin,
//                            UserId = adminId
//                        }
//                    });
//                }

//                var (token, user) = await _userService.login(loginRequest.Email, loginRequest.Password);

//                if (user == null)
//                {
//                    return Unauthorized("Invalid email or password.");
//                }

//                return Ok(new
//                {
//                    Token = token,
//                    User = new
//                    {
//                        UTEmail = user.UTEmail,
//                        Role = user.role,
//                        UserId = user.Id
//                    }
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
//            }
//        }
//    }
//}

