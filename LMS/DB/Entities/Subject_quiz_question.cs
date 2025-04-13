namespace LMS.DB.Entities
{
    public class Subject_quiz_question
    {
        public Guid Id { get; set; }
        public Guid Subject_QuizId { get; set; }
        public Subject_Quiz Subject_Quiz { get; set; }
        public Guid QuestionId { get; set; }
        public Questions Question { get; set; }
    }
}
