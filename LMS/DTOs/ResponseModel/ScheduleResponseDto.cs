namespace LMS.DTOs.ResponseModel
{
    public class ScheduleResponseDto
    {
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public int ClassSchedule { get; set; }  // This could be an enum, but using int for now
        public List<ScheduleDetailResponseDto> ScheduleDetail { get; set; }
        public HolidayDetailResponseDto HolidayDetail { get; set; }
    }
}
