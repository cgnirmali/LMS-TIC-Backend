using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        // Add Lecturer
        [HttpPost("Add_Lecturer")]
        public async Task<IActionResult> AddLecturer([FromBody] LecturerRequest lecturerRequest, [FromQuery] UserStaff_LectureRequest userStaff_LectureRequest)
        {
            try
            {
                string token = await _lecturerService.AddLecturer(lecturerRequest, userStaff_LectureRequest);
                return Ok(new
                {
                    status = "success",
                    message = "Lecturer created successfully",
                    token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Get All Lecturers
        [HttpGet("Get_All_Lecturer")]
        public async Task<IActionResult> GetAllLecturer()
        {
            try
            {
                var lecturers = await _lecturerService.GetAllLecturer();
                return Ok(new { status = "success", data = lecturers });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Get Lecturer By ID
        [HttpGet("Get_Lecturer/{id}")]
        public async Task<IActionResult> GetLecturerById(Guid id)
        {
            try
            {
                var lecturer = await _lecturerService.GetLecturerById(id);
                if (lecturer == null)
                    return NotFound(new { status = "error", message = "Lecturer not found." });

                return Ok(new { status = "success", data = lecturer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Update Lecturer
        [HttpPut("Update_Lecturer/{id}")]
        public async Task<IActionResult> UpdateLecturer(Guid id, [FromBody] LecturerRequest lecturerRequest)
        {
            try
            {
                await _lecturerService.UpdateLecturer(id, lecturerRequest);
                return Ok(new { status = "success", message = "Lecturer updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // Soft Delete Lecturer
        [HttpDelete("Delete_Lecturer/{id}")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            try
            {
                await _lecturerService.DeleteLecturer(id);
                return Ok(new { status = "success", message = "Lecturer deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }
    }
}
