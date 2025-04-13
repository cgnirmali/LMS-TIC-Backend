using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;

namespace LMS.Repositories.Implementation
{
    public class OptionRepository:IOptionRepository
    {
        private readonly AppDbContext _context;

        public OptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Option option)
        {
            // Check if the QuestionId exists in the Questions table
          var questionExists = _context.Questionses
    .Any(q => q.Id == option.QuestionId);

if (!questionExists)
{
    throw new Exception($"No question found with ID {option.QuestionId}. Ensure the QuestionId is valid.");
}

            _context.Options.Add(option);
            _context.SaveChanges();
        }


        public void Update(Option option)
        {
            _context.Options.Update(option);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var option = _context.Options.Find(id);
            if (option != null)
            {
                _context.Options.Remove(option);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Option> GetByQuestionId(Guid questionId)
        {
            return _context.Options.Where(o => o.QuestionId == questionId).ToList();
        }
    }
}
