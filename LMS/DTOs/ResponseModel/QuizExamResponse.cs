namespace LMS.DTOs.ResponseModel
{
    public class QuizExamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan DurationInHours { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

       
        public List<SubjectResponse> Subjects { get; set; }
    }
}
