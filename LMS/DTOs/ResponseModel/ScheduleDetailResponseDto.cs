namespace LMS.DTOs.ResponseModel
{
    public class ScheduleDetailResponseDto
    {

        public Guid ScheduleDetailsId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Description { get; set; }
        public int cellNumber { get; set; }
        public int  TypeOfClass { get; set; }  // For example: "Lap", "Session", etc.
        public string GroupId { get; set; }

        public string Name { get; set; }

        // Assuming GroupId is a GUID but returned as string
    }
}
