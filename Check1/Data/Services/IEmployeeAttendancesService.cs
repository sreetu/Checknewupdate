using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;

namespace Check1.Data.Services
{
    public interface IEmployeeAttendancesService:IEntityBaseRepository<EmployeeAttendance>
    {
        Task<EmployeeAttendance> GetEmployeeAttendanceByIdAsync(int id);
        Task<NewEmployeeAttendanceDropdownsVM> GetNewEmployeeAttendanceDropdownsValues();
        Task AddNewEmployeeAttendanceAsync(NewEmployeeAttendanceVM data);
        Task UpdateEmployeeAttendanceAsync(NewEmployeeAttendanceVM data);
    }
}
