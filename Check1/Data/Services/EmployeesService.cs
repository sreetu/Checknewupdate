using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Check1.Data.Services
{
    public class EmployeesService : EntityBaseRepository<Employee>, IEmployeesService
    {
        private readonly AppDbContext _context;
        public EmployeesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewEmployeeAsync(NewEmployeeVM data)
        {
            var newEmployee = new Employee()
            {
                Name = data.Name,                
                DepartmentId = data.DepartmentId,               
            };
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            //Add Employee Actors
            /*foreach (var actorId in data.ActorIds)
            {
                var newActorEmployee = new Actor_Employee()
                {
                    EmployeeId = newEmployee.Id,
                    ActorId = actorId
                };
                await _context.Actors_Employees.AddAsync(newActorEmployee);
            }
            await _context.SaveChangesAsync();*/
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employeeDetails = await _context.Employees
                .Include(c => c.Department)
                .FirstOrDefaultAsync(n => n.Id == id);

            return employeeDetails;
        }

        public async Task<NewEmployeeDropdownsVM> GetNewEmployeeDropdownsValues()
        {
            var response = new NewEmployeeDropdownsVM() {
                Departments = await _context.Departments.OrderBy(n => n.Name).ToListAsync(),
               };

            return response;
        }

        public async Task UpdateEmployeeAsync(NewEmployeeVM data)
        {
            var dbEmployee = await _context.Employees.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbEmployee != null)
            {
                dbEmployee.Name = data.Name;
                dbEmployee.DepartmentId = data.DepartmentId;
                await _context.SaveChangesAsync();
            }

           /* //Remove existing actors
            var existingActorsDb = _context.Actors_Employees.Where(n => n.EmployeeId == data.Id).ToList();
            _context.Actors_Employees.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Employee Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorEmployee = new Actor_Employee()
                {
                    EmployeeId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Employees.AddAsync(newActorEmployee);
            }*/
            await _context.SaveChangesAsync();
        }

    }
}
