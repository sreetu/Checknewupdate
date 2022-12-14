using Check1.Models;

namespace Check1.Data.ViewModels
{
    public class NewEmployeeAttendanceDropdownsVM
    {
        public NewEmployeeAttendanceDropdownsVM()
        {

            Employees = new List<Employee>();

        }


        public List<Employee>? Employees { get; set; }
    }
}
