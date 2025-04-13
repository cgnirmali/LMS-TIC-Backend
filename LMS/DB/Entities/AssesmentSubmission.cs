using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class AssesmentSubmission
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime SubmissionDate { get; set; }
        public string URL { get; set; }
        public  int Score { get; set; }
        public SubmissionStatus SubmissionStatus { get; set; }

        public Guid StudentId { get; set; }
        public Guid AssesmentId { get; set; }

        //navigation 
        public Student Students { get; set; }
        public Assesment Assesment { get; set; }


    }
}
