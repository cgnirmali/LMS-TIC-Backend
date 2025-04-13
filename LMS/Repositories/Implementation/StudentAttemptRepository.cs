using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DB;
using Microsoft.EntityFrameworkCore;
using LMS.Repositories.Interfaces;

namespace LMS.Repositories.Implementation
{
    public class StudentAttemptRepository:IStudentAttemptRepository
    {
        private readonly AppDbContext _context;

        public StudentAttemptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Questions>> GetQuestionsByQuizExamIdAsync(Guid quizExamId)
        {
            return await _context.Subjects_quiz_questions
                .Include(sqq => sqq.Question)
                    .ThenInclude(q => q.Options)
                .Where(sqq => sqq.Subject_Quiz.QuizExamId == quizExamId)
                .Select(sqq => sqq.Question)
                .ToListAsync();
        }

        public async Task<StudentAttempts> EnrollStudentAsync(Guid studentId, Guid quizExamId)
        {
            var attempt = new StudentAttempts
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                StartTime = DateTime.UtcNow,
                Status = Status.Ongoing,
                StudentId = studentId,
                QuizExamId = quizExamId
            };
            _context.StudentAttemptses.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

        public async Task<StudentAttempts> GetStudentAttemptByIdAsync(Guid attemptId)
        {
            return await _context.StudentAttemptses.FirstOrDefaultAsync(sa => sa.Id == attemptId);
        }

        public async Task<List<Option>> GetOptionsByQuestionIdAsync(Guid questionId)
        {
            return await _context.Options
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<StudentAttempts> GetStudentAttemptByStudentIdAndQuizIdAsync(Guid studentId, Guid quizExamId)
        {
            return await _context.StudentAttemptses
                .FirstOrDefaultAsync(sa => sa.StudentId == studentId && sa.QuizExamId == quizExamId);
        }
    }
}
