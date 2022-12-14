using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;

namespace Check1.Data.Services
{
    public interface IStudentAttendancesService:IEntityBaseRepository<StudentAttendance>
    {
        Task<StudentAttendance> GetStudentAttendanceByIdAsync(int id);
        Task<NewStudentAttendanceDropdownsVM> GetNewStudentAttendanceDropdownsValues();
        Task AddNewStudentAttendanceAsync(NewStudentAttendanceVM data);
        Task UpdateStudentAttendanceAsync(NewStudentAttendanceVM data);
    }
}
