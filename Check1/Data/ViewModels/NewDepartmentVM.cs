using Check1.Models;
using System.ComponentModel.DataAnnotations;

namespace Check1.Data.ViewModels
{
    public class NewDepartmentVM
    {
        public int Id { get; set; }

        [Display(Name = "Department name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        //public List<Employee> Employees { get; set; }
    }
}
