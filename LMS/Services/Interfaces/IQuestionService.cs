using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IQuestionService
    {
        IEnumerable<QuestionResponse> GetAllQuestions();
        QuestionResponse GetQuestionById(Guid id);
        Guid AddQuestion(CreateQuestionRequest request);
        void AddOptionsToQuestion(Guid questionId, List<AddOptionRequest> options);
        void UpdateQuestion(Guid id, UpdateQuestionRequest request);
        void DeleteQuestion(Guid id);

        // 🔹 New method
        IEnumerable<QuestionResponse> GetQuestionsBySubjectId(Guid subjectId);
    }
}
