using Check1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Check1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Course>().HasKey(am => new
            {
                am.StudentId,
                am.CourseId
            });

            modelBuilder.Entity<Student_Course>().HasOne(m => m.Student).WithMany(am => am.Student_Courses).HasForeignKey(m => m.StudentId);
            modelBuilder.Entity<Student_Course>().HasOne(m => m.Course).WithMany(am => am.Student_Courses).HasForeignKey(m => m.CourseId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student_Course> Student_Courses { get; set; }
    }

}
