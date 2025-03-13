using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class MarkingAttendence
    {
        public Guid Id { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public Guid AttendanceId { get; set; }

        //attendenceid foreignkey
        public Attendance Attendance { get; set; }

    }
}
