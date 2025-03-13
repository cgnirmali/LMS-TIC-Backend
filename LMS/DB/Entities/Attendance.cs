using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Time { get; set; }

        public Guid StudentId { get; set; }

        //navigation property

        public Student Student { get; set; }
        public MarkingAttendence MarkingAttendence { get; set; }


    }
}
