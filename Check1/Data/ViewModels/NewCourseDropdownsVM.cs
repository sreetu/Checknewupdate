using Check1.Models;

namespace Check1.Data.ViewModels
{
    public class NewCourseDropdownsVM
    {
        public NewCourseDropdownsVM()
        {
            Students=new List<Student>();
        }
        public List<Student> Students { get; set; }
    }
}
