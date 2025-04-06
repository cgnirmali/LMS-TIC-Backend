namespace LMS.DB.Entities
{
    public class Holiday
    {
        public Guid Id { get; set; }

        public string holiday { get; set; }

        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
