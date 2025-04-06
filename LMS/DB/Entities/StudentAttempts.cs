using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class StudentAttempts
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public Status Status { get; set; }

        public Guid QuizExamId { get; set; }

        public Guid StudentId { get; set; }

        // navigation property
        public QuizExam QuizExam { get; set; }
        public Student Student { get; set; }


    }
}
