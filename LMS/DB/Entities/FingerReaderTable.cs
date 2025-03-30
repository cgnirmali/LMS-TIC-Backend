using LMS.Assets.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.DB.Entities
{
    public class FingerReaderTable
    {

        [Key]
        public Guid FingerReaderID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UTNumber { get; set; }
        public Student Student { get; set; }

        public DateTime Time { get; set; }  

 
        //public Attendance Attendance { get; set; }
    }


  

}
