using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {


        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("Add_Staff")]
        public async Task<IActionResult> AddStaff([FromBody] StaffRequest staffRequest, [FromQuery] UserStaff_LectureRequest userStaff_LectureRequest)
        {
            try
            {
                string token = await _staffService.AddStaff(staffRequest, userStaff_LectureRequest);
                return Ok(new
                {
                    status = "success",
                    message = "User created successfully",
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }




    }
}