using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
 

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        { 
           
            _userService = userService;
        
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email , string password)
        {
            try
            {
                var token = await _userService.loginUser(email, password);
                return Ok(new { status = "success", message = "Login successful", token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }



    }
}
