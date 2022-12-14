
using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Data.Services
{
    public interface IEmployeesService:IEntityBaseRepository<Employee>
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<NewEmployeeDropdownsVM> GetNewEmployeeDropdownsValues();
        Task AddNewEmployeeAsync(NewEmployeeVM data);
        Task UpdateEmployeeAsync(NewEmployeeVM data);

    }
}
