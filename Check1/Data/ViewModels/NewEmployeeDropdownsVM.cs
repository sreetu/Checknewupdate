
using Check1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Data.ViewModels
{
    public class NewEmployeeDropdownsVM
    {
        public NewEmployeeDropdownsVM()
        {

            Departments = new List<Department>();
            
        }

       
        public List<Department> Departments { get; set; }

    }
}
