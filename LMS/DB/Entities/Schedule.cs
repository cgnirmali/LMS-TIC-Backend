using LMS.Assets.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class Schedule
    {

        [Key]
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }

        public ClassSchedule ClassSchedule { get; set; }


        public Holiday Holiday { get; set; }

        public ScheduleDetail ScheduleDetail { get; set; }
    }
}
