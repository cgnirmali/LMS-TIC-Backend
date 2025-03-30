using LMS.Assets.Enums;

namespace LMS.DB.Entities
{
    public class Attendance
    {
        public Guid AttendanceId { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }

        public Guid FingerReaderID { get; set; }
        public FingerReaderTable FingerReaderTable { get; set; }


    }
}
