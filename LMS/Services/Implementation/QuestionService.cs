using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Org.BouncyCastle.Crypto;

namespace LMS.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;

        public QuestionService(IQuestionRepository questionRepository, IOptionRepository optionRepository)
        {
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }
        // Step 1: Add the question (without options)
        public Guid AddQuestion(CreateQuestionRequest request)
        {
            var question = new Questions
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Question = request.Question,
                SubjectId = request.SubjectId
            };

            // Add the question to the repository
            _questionRepository.Add(question);
           
            return question.Id;
        }

        // Step 2: Add options to an existing question using the questionId from the request
        public void AddOptionsToQuestion(Guid questionId, List<AddOptionRequest> options)
            {
                var question = _questionRepository.GetById(questionId);
                if (question == null) throw new Exception("Question not found");

                // Add each option to the database, linking it to the specified questionId
                foreach (var optionRequest in options)
                {
                    var option = new Option
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        Options = optionRequest.Options,
                        IsCorrect = optionRequest.IsCorrect,
                        QuestionId = questionId  // Link the option to the question
                    };

                    // Add the option to the repository
                    _optionRepository.Add(option);
                }
            }

            // Other methods (GetAllQuestions, GetQuestionById, etc.) remain unchanged.
        


        // Get all questions
        public IEnumerable<QuestionResponse> GetAllQuestions()
        {
            var questions = _questionRepository.GetAll();
            var questionResponses = new List<QuestionResponse>();

            foreach (var question in questions)
            {
                // Manually map Questions to QuestionResponse
                var questionResponse = new QuestionResponse
                {
                    Id = question.Id,
                    CreatedDate = question.CreatedDate,
                    Question = question.Question,
                    SubjectId = question.SubjectId,
                    Options = question.Options.Select(o => new OptionResponse
                    {
                        Id = o.Id,
                        CreatedDate = o.CreatedDate,
                        Options = o.Options,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };

                questionResponses.Add(questionResponse);
            }

            return questionResponses;
        }

        // Get a question by ID
        public QuestionResponse GetQuestionById(Guid id)
        {
            var question = _questionRepository.GetById(id);
            if (question == null) return null;

            // Manually map Questions to QuestionResponse
            var questionResponse = new QuestionResponse
            {
                Id = question.Id,
                CreatedDate = question.CreatedDate,
                Question = question.Question,
                SubjectId = question.SubjectId,
                Options = question.Options.Select(o => new OptionResponse
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    Options = o.Options,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };

            return questionResponse;
        }

        // Update a question and its options
        public void UpdateQuestion(Guid id, UpdateQuestionRequest request)
        {
            var question = _questionRepository.GetById(id);
            if (question == null) throw new Exception("Question not found");

            // Manually map the UpdateQuestionRequest to the existing question
            question.Question = request.Question;
            question.SubjectId = request.SubjectId;

            // Update the question in the repository
            _questionRepository.Update(question);

            // Update options (replacing old options with new ones)
            foreach (var optionRequest in request.Options)
            {
                var option = new Option
                {
                    Id = optionRequest.Id,  // Use the provided option ID for updates
                    CreatedDate = DateTime.UtcNow,
                    Options = optionRequest.Options,
                    IsCorrect = optionRequest.IsCorrect,
                    QuestionId = question.Id // Link to the question
                };

                _optionRepository.Update(option); // Update the option in the repository
            }
        }

        // Delete a question and its options
        public void DeleteQuestion(Guid id)
        {
            var question = _questionRepository.GetById(id);
            if (question == null) throw new Exception("Question not found");

            // Delete all options associated with this question
            var options = _optionRepository.GetByQuestionId(id);
            foreach (var option in options)
            {
                _optionRepository.Delete(option.Id);
            }

            // Delete the question
            _questionRepository.Delete(id);
        }

        // Filter questions by subject ID
        public IEnumerable<QuestionResponse> GetQuestionsBySubjectId(Guid subjectId)
        {
            var questions = _questionRepository.GetQuestionsBySubjectId(subjectId);
            var questionResponses = new List<QuestionResponse>();

            foreach (var question in questions)
            {
                var questionResponse = new QuestionResponse
                {
                    Id = question.Id,
                    CreatedDate = question.CreatedDate,
                    Question = question.Question,
                    SubjectId = question.SubjectId,
                    Options = question.Options.Select(o => new OptionResponse
                    {
                        Id = o.Id,
                        CreatedDate = o.CreatedDate,
                        Options = o.Options,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };

                questionResponses.Add(questionResponse);
            }

            return questionResponses;
        }
    }
}
