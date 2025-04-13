using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQuizController : ControllerBase
    {
        private readonly IStudentAttemptService _service;

        public StudentQuizController(IStudentAttemptService service)
        {
            _service = service;
        }

        [HttpGet("questions/{quizExamId}")]
        public async Task<IActionResult> GetQuestions(Guid quizExamId)
        {
            var questions = await _service.GetQuestionsByQuizExamIdAsync(quizExamId);
            return Ok(questions);
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent(Guid studentId, Guid quizExamId)
        {
            var attempt = await _service.EnrollStudentInQuizAsync(studentId, quizExamId);
            return Ok(attempt);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizRequest request)
        {
            var result = await _service.SubmitQuizAndScoreAsync(request);
            return Ok(result);
        }

        [HttpGet("score")]
        public async Task<IActionResult> GetScore(Guid studentId, Guid quizExamId)
        {
            var result = await _service.GetScoreAsync(studentId, quizExamId);
            return Ok(result);
        }
    }
}
