using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Repositories.Interfaces
{
    public interface IQuizExamRepository
    {
        Task<Guid> CreateQuizExamAsync(CreateQuizExamRequest request);
        Task AssignSubjectsAsync(AssignSubjectRequest request);
        Task AssignQuestionsAsync(AssignQuestionsRequest request);
        Task<List<QuizExamResponse>> GetAllQuizExamsAsync();
        Task<QuizExamResponse> GetQuizExamByIdAsync(Guid id);
    }
}
