using LMS.DB;
using LMS.DB.Entities;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class QuestionRepository:IQuestionRepository
    {
        private readonly AppDbContext _context;

      

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Questions> GetAll()
        {
            return _context.Questionses.Include(q => q.Options).ToList();
        }

        public Questions GetById(Guid id)
        {
            return _context.Questionses.Include(q => q.Options).FirstOrDefault(q => q.Id == id);
        }

        public void Add(Questions question)
        {
            // Step 1: Add the question first (without options)
            _context.Questionses.Add(question);
            _context.SaveChanges(); // This assigns a valid Id to the question

            // Step 2: Add the options now that the question.Id is valid
            if (question.Options != null && question.Options.Any())
            {
                foreach (var option in question.Options)
                {
                    option.QuestionId = question.Id;  // Set the foreign key properly
                    _context.Options.Add(option);     // Queue option insert
                }

                _context.SaveChanges(); // Save all options
            }
        }


        public void Update(Questions question)
        {
            // Update the question
            _context.Questionses.Update(question);

            // Update options if they exist
            if (question.Options != null && question.Options.Any())
            {
                foreach (var option in question.Options)
                {
                    if (option.Id == Guid.Empty) // If the option is new, add it
                    {
                        option.QuestionId = question.Id; // Link to the question
                        _context.Options.Add(option);    // Add to Options table
                    }
                    else // If the option already exists, update it
                    {
                        _context.Options.Update(option);
                    }
                }
            }

            _context.SaveChanges(); // Save changes to the database
        }

        public void Delete(Guid id)
        {
            var question = _context.Questionses.Find(id);
            if (question != null)
            {
                _context.Questionses.Remove(question); // Remove the question
                                                       // Optionally, you can remove related options here as well
                var options = _context.Options.Where(o => o.QuestionId == id).ToList();
                _context.Options.RemoveRange(options); // Remove all options related to this question
                _context.SaveChanges(); // Save changes to the database
            }
        }

        // 🔹 New method
        public IEnumerable<Questions> GetQuestionsBySubjectId(Guid subjectId)
        {
            return _context.Questionses
                           .Include(q => q.Options)
                           .Where(q => q.SubjectId == subjectId)
                           .ToList();
        }
    }
}
