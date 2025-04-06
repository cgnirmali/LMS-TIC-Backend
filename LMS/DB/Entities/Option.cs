namespace LMS.DB.Entities
{
    public class Option
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Options { get; set; }
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
      
    }
}
