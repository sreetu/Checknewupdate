using Check1.Data.Services;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Check1.Controllers
{
    public class EmployeeAttendancesController : Controller
    {
        private readonly IEmployeeAttendancesService _service;

        public EmployeeAttendancesController(IEmployeeAttendancesService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            var allEmployeeAttendances = await _service.GetAllAsync(n => n.Employee);
            return View(allEmployeeAttendances);
        }


        /* public async Task<IActionResult> Filter(string searchString)
         {
             var allEmployeeAttendances = await _service.GetAllAsync(n => n.Employee);

             if (!string.IsNullOrEmpty(searchString))
             {
                 //var filteredResult = allEmployees.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                 var filteredResultNew = allEmployeeAttendances.Where(n => string.Equals(n.AttendanceStatus, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                 return View("Index", filteredResultNew);
             }

             return View("Index", allEmployees);
         }
        */
        //GET: EmployeeAttendances/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var EmployeeAttendanceDetail = await _service.GetEmployeeAttendanceByIdAsync(id);
            return View(EmployeeAttendanceDetail);
        }

        //GET: EmployeeAttendances/Create
        public async Task<IActionResult> Create()
        {
            var EmployeeAttendanceDropdownsData = await _service.GetNewEmployeeAttendanceDropdownsValues();

            ViewBag.Employees = new SelectList(EmployeeAttendanceDropdownsData.Employees, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewEmployeeAttendanceVM EmployeeAttendance)
        {
            if (!ModelState.IsValid)
            {
                var EmployeeAttendanceDropdownsData = await _service.GetNewEmployeeAttendanceDropdownsValues();

                ViewBag.Employees = new SelectList(EmployeeAttendanceDropdownsData.Employees, "Id","Name");
                return View(EmployeeAttendance);
            }

            await _service.AddNewEmployeeAttendanceAsync(EmployeeAttendance);
            return RedirectToAction(nameof(Index));
        }


        //GET: EmployeeAttendances/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var EmployeeAttendanceDetails = await _service.GetEmployeeAttendanceByIdAsync(id);
            if (EmployeeAttendanceDetails == null) return View("NotFound");

            var response = new NewEmployeeAttendanceVM()
            {
                Id = EmployeeAttendanceDetails.Id,
                CurrDate = EmployeeAttendanceDetails.CurrDate,
                AttendanceStatus=EmployeeAttendanceDetails.AttendanceStatus,
                EmployeeId = EmployeeAttendanceDetails.EmployeeId,
            };

            var EmployeeAttendanceDropdownsData = await _service.GetNewEmployeeAttendanceDropdownsValues();
            ViewBag.Employees = new SelectList(EmployeeAttendanceDropdownsData.Employees, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewEmployeeAttendanceVM EmployeeAttendance)
        {
            if (id != EmployeeAttendance.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var EmployeeAttendanceDropdownsData = await _service.GetNewEmployeeAttendanceDropdownsValues();

                ViewBag.Employees = new SelectList(EmployeeAttendanceDropdownsData.Employees, "Id", "Name");
                return View(EmployeeAttendance);
            }

            await _service.UpdateEmployeeAttendanceAsync(EmployeeAttendance);
            return RedirectToAction(nameof(Index));
        }
    }
}
