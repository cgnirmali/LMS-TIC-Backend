using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {


        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHolidaySchedule([FromBody] HolidayRequestDto request)
        {
            try
            {
                var holiday = await _holidayService.CreateHolidayScheduleAsync(request);
                return Ok(holiday);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateScheduleDetail(Guid Id, UpdateHolidayRequestDto request)
        {
            var data = await _holidayService.UpdateHolidayAsync(Id, request);
            return Ok(data);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> getHolidayById(Guid Id)
        {
            var data = await _holidayService.getHolidayById(Id);
            return Ok(data);
        }



        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHoliday(Guid Id)
        {
            var result = await _holidayService.DeleteHoliday(Id);
            if (!result)
            {
                return NotFound(new { message = "Holiday not found" });
            }

            return Ok(new { message = "Holiday deleted successfully" });
        }
    }
}
