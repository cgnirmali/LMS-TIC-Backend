using LMS.Assets.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class LeaveRequest
    {


        [Key]
        public Guid RequestId { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        public int NumOfDays { get; set; }

        public string Reason { get; set; }


        public string UTNumber { get; set; }
        public Student Student { get; set; }


        public LeaveRequestStatus leaveRequestStatus { get; set; }

        public ICollection<LeaveRequestSection> LeaveRequestSection { get; set; }

    }
}
