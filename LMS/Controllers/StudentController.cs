using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        IStudentRepository _studentRepository;
        IStudentService _studentService;
        public StudentController(IUserService userService, IUserRepository userRepository,IStudentRepository studentRepository, IStudentService studentService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _studentService = studentService;
        }

        [HttpPost("register-new-student")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
                if (user == null || user.IsEmailConfirmed == false) return BadRequest("Verify Your Email");

                await _userService.Register(registerRequest);
                return Ok("Student Registered Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Verify-student")]
        public async Task<IActionResult> VerifyRegister(Guid id)
        {
            try
            {
                await _studentRepository.VerifyRegister(id);
                return Ok("Student Registered By Admin");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudent-By-Email")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            try
            {
                var data = await _studentRepository.GetStudentByEmail(email);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-All-Students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var data = await _studentRepository.GetAllStudents(); 
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
