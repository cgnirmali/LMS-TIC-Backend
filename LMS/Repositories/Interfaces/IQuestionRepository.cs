using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Questions> GetAll();
        Questions GetById(Guid id);
        void Add(Questions question);
        void Update(Questions question);
        void Delete(Guid id);

        // 🔹 New method
        IEnumerable<Questions> GetQuestionsBySubjectId(Guid subjectId);
    }
}
