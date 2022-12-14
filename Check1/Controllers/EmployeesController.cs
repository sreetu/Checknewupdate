using Check1.Data;
using Check1.Data.Services;
using Check1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Check1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _service;

        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }

     
        public async Task<IActionResult> Index()
        {
            var allEmployees = await _service.GetAllAsync(n => n.Department);
            return View(allEmployees);
        }

      
        public async Task<IActionResult> Filter(string searchString)
        {
            var allEmployees = await _service.GetAllAsync(n => n.Department);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allEmployees.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allEmployees.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) ).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allEmployees);
        }

        //GET: Employees/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var EmployeeDetail = await _service.GetEmployeeByIdAsync(id);
            return View(EmployeeDetail);
        }

        //GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var EmployeeDropdownsData = await _service.GetNewEmployeeDropdownsValues();

            ViewBag.Departments = new SelectList(EmployeeDropdownsData.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewEmployeeVM Employee)
        {
            if (!ModelState.IsValid)
            {
                var EmployeeDropdownsData = await _service.GetNewEmployeeDropdownsValues();

                ViewBag.Departments = new SelectList(EmployeeDropdownsData.Departments, "Id", "Name");
                return View(Employee);
            }

            await _service.AddNewEmployeeAsync(Employee);
            return RedirectToAction(nameof(Index));
        }


        //GET: Employees/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var EmployeeDetails = await _service.GetEmployeeByIdAsync(id);
            if (EmployeeDetails == null) return View("NotFound");

            var response = new NewEmployeeVM()
            {
                Id = EmployeeDetails.Id,
                Name = EmployeeDetails.Name,
                DepartmentId = EmployeeDetails.DepartmentId,
            };

            var EmployeeDropdownsData = await _service.GetNewEmployeeDropdownsValues();
            ViewBag.Departments = new SelectList(EmployeeDropdownsData.Departments, "Id", "Name");      
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewEmployeeVM Employee)
        {
            if (id != Employee.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var EmployeeDropdownsData = await _service.GetNewEmployeeDropdownsValues();

                ViewBag.Departments = new SelectList(EmployeeDropdownsData.Departments, "Id", "Name");
                return View(Employee);
            }

            await _service.UpdateEmployeeAsync(Employee);
            return RedirectToAction(nameof(Index));
        }

    }
}
