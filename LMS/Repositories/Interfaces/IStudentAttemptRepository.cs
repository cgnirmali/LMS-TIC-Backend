using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IStudentAttemptRepository
    {
        Task<List<Questions>> GetQuestionsByQuizExamIdAsync(Guid quizExamId);
        Task<StudentAttempts> EnrollStudentAsync(Guid studentId, Guid quizExamId);
        Task<StudentAttempts> GetStudentAttemptByIdAsync(Guid attemptId);
        Task<List<Option>> GetOptionsByQuestionIdAsync(Guid questionId);
        Task SaveChangesAsync();
        Task<StudentAttempts> GetStudentAttemptByStudentIdAndQuizIdAsync(Guid studentId, Guid quizExamId);
    }
}
