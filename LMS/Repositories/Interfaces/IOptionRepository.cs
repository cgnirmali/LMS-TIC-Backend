using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IOptionRepository
    {
        void Add(Option option);
        void Update(Option option);
        void Delete(Guid id);
        IEnumerable<Option> GetByQuestionId(Guid questionId);
    }
}
