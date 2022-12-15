using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Check1.Data.Base;

namespace Check1.Models
{
    public class Student : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        /*public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        */
        public List<StudentAttendance>? StudentAttendances { get; set; }
        public List<Student_Course>? Student_Courses { get; set; }
    }
}
