namespace LMS.DB.Entities
{
    public class Questions
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Question { get; set; }

        public Guid QuestionId { get; set; }

        //navigation property
        public Subject Subject { get; set; }

       

        public ICollection<Option> Options { get; set; }


    }
}
