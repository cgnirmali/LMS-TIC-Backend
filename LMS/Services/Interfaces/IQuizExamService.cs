using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IQuizExamService
    {
        Task<Guid> CreateQuizExamAsync(CreateQuizExamRequest request);
        Task AssignSubjectsAsync(AssignSubjectRequest request);
        Task AssignQuestionsAsync(AssignQuestionsRequest request);
        Task<List<QuizExamResponse>> GetAllQuizExamsAsync();
        Task<QuizExamResponse> GetQuizExamByIdAsync(Guid id);
    }
}
