using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectRequest request)
        {
            var response = await _subjectService.CreateSubjectAsync(request);
            //return CreatedAtAction(nameof(GetSubjectById), new { id = response.Id }, response);
            return Ok(response);
        }

        [HttpGet("{CourseId}")]
        public async Task<IActionResult> GetSubjectByCourseId(Guid CourseId)
        {
            var response = await _subjectService.GetSubjectByCourseIdAsync(CourseId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var response = await _subjectService.GetAllSubjectsAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] SubjectRequest request)
        {
            var response = await _subjectService.UpdateSubjectAsync(id, request);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var success = await _subjectService.DeleteSubjectAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }

}
