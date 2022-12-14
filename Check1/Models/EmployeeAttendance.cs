using Check1.Data.Base;
using Check1.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Check1.Models
{
    public class EmployeeAttendance:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string CurrDate { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public AttendanceStatus? AttendanceStatus { get; set; }
    }
}
