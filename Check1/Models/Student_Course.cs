using Check1.Data.Base;

namespace Check1.Models
{
    public class Student_Course
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
