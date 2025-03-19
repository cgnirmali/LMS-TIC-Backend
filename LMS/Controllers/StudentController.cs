using LMS.DB;
using LMS.DB.Entities;
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
        private readonly AppDbContext _Context;
        public StudentController(IUserService userService, IUserRepository userRepository,IStudentRepository studentRepository, IStudentService studentService,AppDbContext appDbContext)
        {
            _userService = userService;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _studentService = studentService;
            _Context = appDbContext;

        }

        [HttpPost("Add-new-student")]
        public async Task<IActionResult> AddNewStudent(StudentRequest studentRequest)
        {
            try
            {
                var student = await _studentRepository.GetStudentByEmail(studentRequest.UserEmail);
                if (student != null) return BadRequest("Student Already Exist");

                await _studentService.AddNewStudent(studentRequest);
                return Ok("Student Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Update-Student/{studentId}")]
        public async Task<IActionResult> UpdateStudent(Guid studentId, [FromBody] StudentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var updatedStudent = new Student
            {
                UTNumber = request.UTNumber,
                NIC = request.NIC,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserEmail = request.UserEmail,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Address = request.Address
            };

            bool isUpdated = await _studentService.UpdateStudentAsync(studentId, updatedStudent, request.NewPassword , request.UTEmail);

            if (isUpdated)
            {
                return Ok("Student and password updated successfully.");
            }
            else
            {
                return NotFound("Student not found.");
            }
        }


        [HttpDelete("DeleteStudents/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync(); // ✅ Start transaction
            try
            {
   
              var data = await _studentService.DeleteStudent(id);
                await transaction.CommitAsync(); // ✅ Commit if everything is successful
                return Ok(data);
             
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // ❌ Rollback if any error occurs
                throw; // Rethrow the exception
            }

        }


        [HttpGet("Get-Student-By-Id/{studentId}")]
        public async Task<IActionResult> GetStudentById(Guid studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student); // Return student details including User info
        }


        [HttpGet("GetStudent-By-Email")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            try
            {
                var data = await _studentService.GetStudentByEmail(email);
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
                var data = await _studentService.GetAllStudents(); 
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("register-new-student")]
        //public async Task<IActionResult> Register(RegisterRequest registerRequest)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
        //        if (user == null || user.IsEmailConfirmed == false) return BadRequest("Verify Your Email");

        //        await _userService.Register(registerRequest);
        //        return Ok("Student Registered Succesfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost("Verify-student")]
        //public async Task<IActionResult> VerifyRegister(Guid id)
        //{
        //    try
        //    {
        //        await _studentRepository.VerifyRegister(id);
        //        return Ok("Student Registered By Admin");
        //    }
        //    catch (Exception ex) 
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


    }
}
