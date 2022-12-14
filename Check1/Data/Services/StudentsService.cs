using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;

namespace Check1.Data.Services
{
    public class StudentsService : EntityBaseRepository<Student>, IStudentsService
    {
        private readonly AppDbContext _context;
        public StudentsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewStudentAsync(NewStudentVM data)
        {
            var newStudent = new Student()
            {
                Name = data.Name,
               // DepartmentId = data.DepartmentId,
            };
            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var studentDetails = await _context.Students
               // .Include(c => c.Department)
                .FirstOrDefaultAsync(n => n.Id == id);

            return studentDetails;
        }

        public async Task<NewStudentDropdownsVM> GetNewStudentDropdownsValues()
        {
            var response = new NewStudentDropdownsVM()
            {
               // Departments = await _context.Departments.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateStudentAsync(NewStudentVM data)
        {
            var dbStudent = await _context.Students.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbStudent != null)
            {
                dbStudent.Name = data.Name;
              //  dbStudent.DepartmentId = data.DepartmentId;
                await _context.SaveChangesAsync();
            }

          
            await _context.SaveChangesAsync();
        }
    }
}
