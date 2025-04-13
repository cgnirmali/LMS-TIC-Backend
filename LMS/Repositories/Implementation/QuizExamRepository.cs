using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class QuizExamRepository:IQuizExamRepository
    {
        private readonly AppDbContext _context;

        public QuizExamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateQuizExamAsync(CreateQuizExamRequest request)
        {
            var exam = new QuizExam
            {
                Id = Guid.NewGuid(),
                Name = request.ExamName,
                DurationInHours = request.DurationInHours,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow
            };

            _context.QuizExams.Add(exam);
            await _context.SaveChangesAsync();

            return exam.Id;
        }

        public async Task AssignSubjectsAsync(AssignSubjectRequest request)
        {
            foreach (var subjectId in request.SubjectIds)
            {
                var subjectQuiz = new Subject_Quiz
                {
                    Id = Guid.NewGuid(),
                    QuizExamId = request.ExamId,
                    SubjectId = subjectId
                };

                _context.Subjects_Quizes.Add(subjectQuiz);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AssignQuestionsAsync(AssignQuestionsRequest request)
        {
            foreach (var questionId in request.QuestionIds)
            {
                var link = new Subject_quiz_question
                {
                    Id = Guid.NewGuid(),
                    Subject_QuizId = request.SubjectQuizId,
                    QuestionId = questionId
                };

                _context.Subjects_quiz_questions.Add(link);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<QuizExamResponse>> GetAllQuizExamsAsync()
        {
            return await _context.QuizExams
                .Select(q => new QuizExamResponse
                {
                    Id = q.Id,
                    Name = q.Name,
                    Description = q.Description,
                    DurationInHours = q.DurationInHours,
                    CreatedDate = q.CreatedDate
                }).ToListAsync();
        }

        public async Task<QuizExamResponse> GetQuizExamByIdAsync(Guid id)
        {
            var q = await _context.QuizExams.FirstOrDefaultAsync(e => e.Id == id);
            if (q == null) return null;

            return new QuizExamResponse
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                DurationInHours = q.DurationInHours,
                CreatedDate = q.CreatedDate
            };
        }
    }
}
