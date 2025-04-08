using LMS.Assets.Enums;

namespace LMS.DTOs.RequestModel
{
    public class ScheduleDetailRequestDto
    {

        public string StartTime { get; set; } // e.g., "08:00:00"
        public string EndTime { get; set; }   // e.g., "10:00:00"
        public string? Description { get; set; }
        public TypeOfClass TypeOfClass { get; set; }
        public int cellNumber { get; set; }
        public Guid GroupId { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
