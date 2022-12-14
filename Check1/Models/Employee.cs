using Check1.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Check1.Models
{
    public class Employee:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        List<EmployeeAttendance>? EmployeeAttendances { get; set; }
    }
}
