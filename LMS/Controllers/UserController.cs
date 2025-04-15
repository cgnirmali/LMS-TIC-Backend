
﻿using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
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

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp(string email)
        {
            if (await _userService.SendOtpAsync(email))
                return Ok(new { message = "OTP sent successfully." });

            return BadRequest(new { message = "Failed to send OTP." });
        }


        [HttpPost("CheckOTP")]
        public async Task<IActionResult> CheckOTP(OtpVerifyDto otpVerifyDto )
        {
            var data = await _userService.VerifyOtpAsync(otpVerifyDto);
          
            return Ok(data);

        }


        

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string email, string password)
        {
            var data = await _userService.ChangePassword(email,password);
            var json = new { message = "PasswordChanged" };
            return Ok(json);
        }





        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var data = await _userService.login(email, password);
                return Ok(new { status = "success", message = "Login successful", data.token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        [HttpDelete("remove-expired")]
        public async Task<IActionResult> RemoveExpiredOtps()
        {
            await _userService.RemoveExpiredOtpsAsync();
            return Ok(new { message = "Expired OTPs deleted successfully." });
        }




    }
}
