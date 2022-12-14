
using Check1.Data.Base;
using Check1.Data.Services;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Data.Services
{
    public class DepartmentsService:EntityBaseRepository<Department>, IDepartmentsService
    {
        private readonly AppDbContext _context;
        public DepartmentsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewDepartmentAsync(NewDepartmentVM data)
        {
            var newDepartment = new Department()
            {
                Name = data.Name,
            };
            await _context.Departments.AddAsync(newDepartment);
            await _context.SaveChangesAsync();

        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var departmentDetails = await _context.Departments
                .FirstOrDefaultAsync(n => n.Id == id);

            return departmentDetails;
        }

        public async Task UpdateDepartmentAsync(NewDepartmentVM data)
        {
            var dbDepartment = await _context.Departments.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbDepartment != null)
            {
                dbDepartment.Name = data.Name;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }

    }
}
