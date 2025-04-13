using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.Implementation
{
    public class QuizExamService : IQuizExamService
    {
        private readonly IQuizExamRepository _quizExamRepository;

        public QuizExamService(IQuizExamRepository quizExamRepository)
        {
            _quizExamRepository = quizExamRepository;
        }

        public async Task<Guid> CreateQuizExamAsync(CreateQuizExamRequest request)
        {
            return await _quizExamRepository.CreateQuizExamAsync(request);
        }

        public async Task AssignSubjectsAsync(AssignSubjectRequest request)
        {
            await _quizExamRepository.AssignSubjectsAsync(request);
        }

        public async Task AssignQuestionsAsync(AssignQuestionsRequest request)
        {
            await _quizExamRepository.AssignQuestionsAsync(request);
        }

        public async Task<List<QuizExamResponse>> GetAllQuizExamsAsync()
        {
            return await _quizExamRepository.GetAllQuizExamsAsync();
        }

        public async Task<QuizExamResponse> GetQuizExamByIdAsync(Guid id)
        {
            return await _quizExamRepository.GetQuizExamByIdAsync(id);
        }
    }
}
