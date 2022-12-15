using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;

namespace Check1.Data.Services
{
    public class CoursesService : EntityBaseRepository<Course>, ICoursesService
    {
        private readonly AppDbContext _context;
        public CoursesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewCourseAsync(NewCourseVM data)
        {
            var newCourse = new Course()
            {
                Name = data.Name,
                Description = data.Description,
                MaxMarks = data.MaxMarks,
                PassMarks = data.PassMarks,
            };
            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();

            //Add Course Students
            foreach (var StudentId in data.StudentIds)
            {
                var newStudentCourse = new Student_Course()
                {
                    CourseId = newCourse.Id,
                    StudentId = StudentId
                };
                await _context.Student_Courses.AddAsync(newStudentCourse);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var CourseDetails = await _context.Courses
                .Include(am => am.Student_Courses).ThenInclude(a => a.Student)
                .FirstOrDefaultAsync(n => n.Id == id);

            return CourseDetails;
        }

        public async Task<NewCourseDropdownsVM> GetNewCourseDropdownsValues()
        {
            var response = new NewCourseDropdownsVM()
            {
                Students = await _context.Students.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateCourseAsync(NewCourseVM data)
        {
            var dbCourse = await _context.Courses.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbCourse != null)
            {
                dbCourse.Name = data.Name;
                dbCourse.Description = data.Description;
                dbCourse.MaxMarks = data.MaxMarks;
                dbCourse.PassMarks= data.PassMarks;
                await _context.SaveChangesAsync();
            }

            //Remove existing Students
            var existingStudentsDb = _context.Student_Courses.Where(n => n.CourseId == data.Id).ToList();
            _context.Student_Courses.RemoveRange(existingStudentsDb);
            await _context.SaveChangesAsync();

            //Add Course Students
            foreach (var StudentId in data.StudentIds)
            {
                var newStudentCourse = new Student_Course()
                {
                    CourseId = data.Id,
                    StudentId = StudentId
                };
                await _context.Student_Courses.AddAsync(newStudentCourse);
            }
            await _context.SaveChangesAsync();
        }
    }
}
