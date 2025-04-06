using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        // Add Staff
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

        // Get All Staff
        [HttpGet("Get_All_Staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                var staffs = await _staffService.GetAllStaff();
                return Ok(new { status = "success", data = staffs });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Get Staff By ID
        [HttpGet("Get_Staff/{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            try
            {
                var staff = await _staffService.GetStaffById(id);
                if (staff == null)
                    return NotFound(new { status = "error", message = "Staff not found." });

                return Ok(new { status = "success", data = staff });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Update Staff
        [HttpPut("Update_Staff/{id}")]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] StaffRequest staffRequest)
        {
            try
            {
                await _staffService.UpdateStaff(id, staffRequest);
                return Ok(new { status = "success", message = "Staff updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Delete Staff (Soft Delete)
        [HttpDelete("Delete_Staff/{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            try
            {
                await _staffService.DeleteStaff(id);
                return Ok(new { status = "success", message = "Staff deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }
    }
}
