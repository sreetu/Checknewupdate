using Check1.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Check1.Models
{
    public class Course:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int MaxMarks { get; set; }

        public int PassMarks { get; set; }
        public List<Student_Course> Student_Courses { get; set; }
    }
}
