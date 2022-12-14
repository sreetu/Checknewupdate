using Check1.Data.Base;
using Check1.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Check1.Models
{
    public class StudentAttendance:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string CurrDate { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public AttendanceStatus? AttendanceStatus { get; set; }
    }
}
