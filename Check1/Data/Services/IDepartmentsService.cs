using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Data.Services
{
    public interface IDepartmentsService:IEntityBaseRepository<Department>
    {
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddNewDepartmentAsync(NewDepartmentVM data);
        Task UpdateDepartmentAsync(NewDepartmentVM data);
    }
}
