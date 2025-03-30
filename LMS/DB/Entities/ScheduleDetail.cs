using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class ScheduleDetail
    {

        [Key]
        public Guid ScheduleDetailsId { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }


        public string? Description { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
