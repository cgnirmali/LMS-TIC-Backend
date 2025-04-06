namespace LMS.DB.Entities
{
    public class LeaveRequestSection
    {

        public Guid RequestSectionId { get; set; }

        public Guid ScheduleDetailsId { get; set; }

        public ScheduleDetail scheduleDetail { get; set; }


        public Guid RequestId { get; set; }

        public LeaveRequest leaveRequest { get; set; }
    }
}
