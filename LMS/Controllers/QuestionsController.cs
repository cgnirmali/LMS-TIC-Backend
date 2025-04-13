using LMS.DTOs.RequestModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = _questionService.GetAllQuestions();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestionById(Guid id)
        {
            var question = _questionService.GetQuestionById(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost("add-question")]
        public IActionResult AddQuestion(CreateQuestionRequest request)
        {
            var questionId = _questionService.AddQuestion(request);
            return Ok(new { message = "Question added successfully.", questionId });
        }

        [HttpPost("add-options")]
        public IActionResult AddOptions([FromBody] CreateOptionRequest request)
        {
            _questionService.AddOptionsToQuestion(request.QuestionId, request.Options);
            return Ok(new { message = "Options added successfully." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(Guid id, UpdateQuestionRequest request)
        {
            _questionService.UpdateQuestion(id, request);
            return Ok(new { message = "Question updated successfully." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(Guid id)
        {
            _questionService.DeleteQuestion(id);
            return Ok(new { message = "Question deleted successfully." });
        }

        // 🔹 New endpoint
        [HttpGet("by-subject/{subjectId}")]
        public IActionResult GetQuestionsBySubjectId(Guid subjectId)
        {
            var questions = _questionService.GetQuestionsBySubjectId(subjectId);
            return Ok(questions);
        }
    }
}
