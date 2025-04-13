using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class StudentAttemptService:IStudentAttemptService
    {
        private readonly IStudentAttemptRepository _repository;

        public StudentAttemptService(IStudentAttemptRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Questions>> GetQuestionsByQuizExamIdAsync(Guid quizExamId)
        {
            return await _repository.GetQuestionsByQuizExamIdAsync(quizExamId);
        }

        public async Task<StudentAttempts> EnrollStudentInQuizAsync(Guid studentId, Guid quizExamId)
        {
            return await _repository.EnrollStudentAsync(studentId, quizExamId);
        }

        public async Task<SubmitQuizResponseDto> SubmitQuizAndScoreAsync(SubmitQuizRequest request)
        {
            var attempt = await _repository.GetStudentAttemptByIdAsync(request.StudentAttemptId);
            if (attempt == null || attempt.StudentId != request.StudentId)
                throw new Exception("Invalid attempt or student ID");

            int correctAnswers = 0;
            foreach (var answer in request.Answers)
            {
                var options = await _repository.GetOptionsByQuestionIdAsync(answer.QuestionId);
                var selected = options.FirstOrDefault(o => o.Id == answer.SelectedOptionId);
                if (selected != null && selected.IsCorrect)
                    correctAnswers++;
            }

            int totalQuestions = request.Answers.Count;
            attempt.Score = correctAnswers;
            attempt.Status = Status.Completed;
            attempt.EndTime = DateTime.UtcNow;
            await _repository.SaveChangesAsync();

            return new SubmitQuizResponseDto
            {
                Message = "Quiz submitted successfully",
                CorrectAnswers = correctAnswers,
                TotalQuestions = totalQuestions,
                Percentage = totalQuestions > 0 ? (double)correctAnswers / totalQuestions * 100 : 0,
                ScoreDisplay = $"{correctAnswers} out of {totalQuestions}"
            };
        }

        public async Task<SubmitQuizResponseDto> GetScoreAsync(Guid studentId, Guid quizExamId)
        {
            var attempt = await _repository.GetStudentAttemptByStudentIdAndQuizIdAsync(studentId, quizExamId);
            if (attempt == null || attempt.Status != Status.Completed)
                throw new Exception("No completed quiz found for this student and exam.");

            var totalQuestions = await _repository.GetQuestionsByQuizExamIdAsync(quizExamId);
            return new SubmitQuizResponseDto
            {
                Message = "Quiz result fetched successfully",
                CorrectAnswers = attempt.Score,
                TotalQuestions = totalQuestions.Count,
                Percentage = totalQuestions.Count > 0 ? (double)attempt.Score / totalQuestions.Count * 100 : 0,
                ScoreDisplay = $"{attempt.Score} out of {totalQuestions.Count}"
            };
        }
    }
}
