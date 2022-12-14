using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;

namespace Check1.Data.Services
{
    public class EmployeeAttendancesService: EntityBaseRepository<EmployeeAttendance>, IEmployeeAttendancesService
    {
        private readonly AppDbContext _context;
        public EmployeeAttendancesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewEmployeeAttendanceAsync(NewEmployeeAttendanceVM data)
        {
            var newEmployeeAttendance = new EmployeeAttendance()
            {
                CurrDate = data.CurrDate,
                AttendanceStatus = data.AttendanceStatus,
                EmployeeId = data.EmployeeId,
            };
            await _context.EmployeeAttendances.AddAsync(newEmployeeAttendance);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAttendance> GetEmployeeAttendanceByIdAsync(int id)
        {
            var employeeDetailsAttendance = await _context.EmployeeAttendances
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(n => n.Id == id);

            return employeeDetailsAttendance;
        }

        public async Task<NewEmployeeAttendanceDropdownsVM> GetNewEmployeeAttendanceDropdownsValues()
        {
            var response = new NewEmployeeAttendanceDropdownsVM()
            {
                Employees = await _context.Employees.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateEmployeeAttendanceAsync(NewEmployeeAttendanceVM data)
        {
            var dbEmployeeAttendance = await _context.EmployeeAttendances.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbEmployeeAttendance != null)
            {
                dbEmployeeAttendance.CurrDate = data.CurrDate;
                dbEmployeeAttendance.AttendanceStatus = data.AttendanceStatus;
                dbEmployeeAttendance.EmployeeId = data.EmployeeId;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
