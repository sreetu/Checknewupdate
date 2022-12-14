using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;

namespace Check1.Data.Services
{
    public class StudentAttendancesService: EntityBaseRepository<StudentAttendance>, IStudentAttendancesService
    {
        private readonly AppDbContext _context;
        public StudentAttendancesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewStudentAttendanceAsync(NewStudentAttendanceVM data)
        {
            var newStudentAttendance = new StudentAttendance()
            {
                CurrDate = data.CurrDate,
                AttendanceStatus = data.AttendanceStatus,
                StudentId = data.StudentId,
            };
            await _context.StudentAttendances.AddAsync(newStudentAttendance);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentAttendance> GetStudentAttendanceByIdAsync(int id)
        {
            var StudentDetailsAttendance = await _context.StudentAttendances
                .Include(c => c.Student)
                .FirstOrDefaultAsync(n => n.Id == id);

            return StudentDetailsAttendance;
        }

        public async Task<NewStudentAttendanceDropdownsVM> GetNewStudentAttendanceDropdownsValues()
        {
            var response = new NewStudentAttendanceDropdownsVM()
            {
                Students = await _context.Students.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateStudentAttendanceAsync(NewStudentAttendanceVM data)
        {
            var dbStudentAttendance = await _context.StudentAttendances.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbStudentAttendance != null)
            {
                dbStudentAttendance.CurrDate = data.CurrDate;
                dbStudentAttendance.AttendanceStatus = data.AttendanceStatus;
                dbStudentAttendance.StudentId = data.StudentId;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
