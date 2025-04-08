using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("generate/{year}/{month}")]
        public async Task<IActionResult> CreateSchedulesForMonth(int year, int month)
        {
            await _scheduleService.CreateSchedulesForMonth(year, month);
            return Ok(new { Message = $"Schedules created for {month}/{year}" });
        }


        [HttpGet("by-month-year")]
        public async Task<IActionResult> GetSchedulesByMonthYear([FromQuery] int year, [FromQuery] int month)
        {
            var schedules = await _scheduleService.GetSchedulesByMonthAndYearAsync(year, month);
            return Ok(schedules);
        }
    }
}
