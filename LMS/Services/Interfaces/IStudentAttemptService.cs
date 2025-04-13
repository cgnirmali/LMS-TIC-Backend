using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IStudentAttemptService
    {
        Task<List<Questions>> GetQuestionsByQuizExamIdAsync(Guid quizExamId);
        Task<StudentAttempts> EnrollStudentInQuizAsync(Guid studentId, Guid quizExamId);
        Task<SubmitQuizResponseDto> SubmitQuizAndScoreAsync(SubmitQuizRequest request);
        Task<SubmitQuizResponseDto> GetScoreAsync(Guid studentId, Guid quizExamId);
    }
}
