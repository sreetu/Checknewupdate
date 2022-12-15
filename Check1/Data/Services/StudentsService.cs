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
            foreach (var courseId in data.CourseIds)
            {
                var newStudentCourse = new Student_Course()
                {
                    StudentId = newStudent.Id,
                    CourseId = courseId
                };
                await _context.Student_Courses.AddAsync(newStudentCourse);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var studentDetails = await _context.Students
                .Include(am => am.Student_Courses).ThenInclude(a => a.Course)
                .FirstOrDefaultAsync(n => n.Id == id);

            return studentDetails;
        }

        public async Task<NewStudentDropdownsVM> GetNewStudentDropdownsValues()
        {
            var response = new NewStudentDropdownsVM()
            {
                Courses = await _context.Courses.OrderBy(n => n.Name).ToListAsync(),
            
            };

            return response;
        }

        public async Task UpdateStudentAsync(NewStudentVM data)
        {
            var dbStudent = await _context.Students.FirstOrDefaultAsync(n => n.Id == data.Id);

            var existingCoursesDb = _context.Student_Courses.Where(n => n.StudentId == data.Id).ToList();
            _context.Student_Courses.RemoveRange(existingCoursesDb);
            await _context.SaveChangesAsync();

            //Add Student Courses
            foreach (var courseId in data.CourseIds)
            {
                var newStudentCourse = new Student_Course()
                {
                    StudentId = data.Id,
                    CourseId = courseId
                };
                await _context.Student_Courses.AddAsync(newStudentCourse);
            }
            await _context.SaveChangesAsync();
        }
    }
}
