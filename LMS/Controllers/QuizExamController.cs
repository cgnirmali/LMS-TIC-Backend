using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizExamController : ControllerBase
    {
        private readonly IQuizExamService _quizExamService;

        public QuizExamController(IQuizExamService quizExamService)
        {
            _quizExamService = quizExamService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuizExam([FromBody] CreateQuizExamRequest request)
        {
            var id = await _quizExamService.CreateQuizExamAsync(request);
            return Ok(new { QuizExamId = id });
        }

        [HttpPost("assign-subjects")]
        public async Task<IActionResult> AssignSubjects([FromBody] AssignSubjectRequest request)
        {
            await _quizExamService.AssignSubjectsAsync(request);
            return Ok("Subjects assigned.");
        }

        [HttpPost("assign-questions")]
        public async Task<IActionResult> AssignQuestions([FromBody] AssignQuestionsRequest request)
        {
            await _quizExamService.AssignQuestionsAsync(request);
            return Ok("Questions assigned.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _quizExamService.GetAllQuizExamsAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var exam = await _quizExamService.GetQuizExamByIdAsync(id);
            return exam == null ? NotFound() : Ok(exam);
        }
    }
}
