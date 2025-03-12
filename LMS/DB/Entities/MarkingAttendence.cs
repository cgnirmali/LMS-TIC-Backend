using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class MarkingAttendence
    {
        public Guid Id { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; } 

        //attendenceid foreignkey
    }
}
