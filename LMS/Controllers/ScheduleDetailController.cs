using LMS.DTOs.RequestModel;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDetailController : ControllerBase
    {
        private readonly IScheduleDetailService _scheduleDetailService;

        public ScheduleDetailController(IScheduleDetailService scheduleDetailService)
        {
            _scheduleDetailService = scheduleDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleDetailRequestDto request)
        {
            try
            {
                var schedule = await _scheduleDetailService.CreateScheduleAsync(request);
                return Ok(schedule);
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


        [HttpPut("{ScheduleDetailsId}")]
        public async Task<IActionResult> UpdateScheduleDetail(Guid ScheduleDetailsId, UpdateScheduleDetailRequestDto request)
        {
            var data = await _scheduleDetailService.UpdateScheduleDetailAsync(ScheduleDetailsId, request);
            return Ok(data);
        }

        [HttpGet("{ScheduleDetailsId}")]
      
        public async Task<IActionResult> getScheduleDetailById(Guid ScheduleDetailsId)
        {

            var data = await _scheduleDetailService.getscheduledetailbyId(ScheduleDetailsId);
            return Ok(data);
        }
    }
}
