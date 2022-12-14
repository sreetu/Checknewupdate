
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Models
{
    public class NewEmployeeVM
    {
        public int Id { get; set; }

        [Display(Name = "Employee name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //Relationships
  

        [Display(Name = "Select a dept")]
        [Required(ErrorMessage = "dept is required")]
        public int DepartmentId { get; set; }

    }
}
