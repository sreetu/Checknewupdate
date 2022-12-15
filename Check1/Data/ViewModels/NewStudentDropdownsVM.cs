using Check1.Models;

namespace Check1.Data.ViewModels
{
    public class NewStudentDropdownsVM
    {
        public NewStudentDropdownsVM()
        {

            Courses = new List<Course>();

        }


        public List<Course> Courses { get; set; }
    }
}
