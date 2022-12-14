using Check1.Data;
using Check1.Data.Services;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Check1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService _service;

        public DepartmentsController(IDepartmentsService service)
        {
            _service = service;
        }

      
        public async Task<IActionResult> Index()
        {
            var allDepartments = await _service.GetAllAsync();
            return View(allDepartments);
        }


        //Get: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewDepartmentVM department)
        {

           if (!ModelState.IsValid) return View(department);
            await _service.AddNewDepartmentAsync(department);
            return RedirectToAction(nameof(Index));
        }
        /*[HttpPost]
        public async Task<IActionResult> Create(string name)
        {

            Department department = new Department();
            try
            {
                department.Name = name;
            }catch(Exception e) { return View(department); }
            //if (!ModelState.IsValid) return View(department);
            await _service.AddAsync(department);
            return RedirectToAction(nameof(Index));
        }*/

        //Get: Departments/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var departmentDetails = await _service.GetDepartmentByIdAsync(id);
            if (departmentDetails == null) return View("NotFound");
            return View(departmentDetails);
        }

        //Get: Departments/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var departmentDetails = await _service.GetDepartmentByIdAsync(id);
            if (departmentDetails == null) return View("NotFound");
            return View(departmentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewDepartmentVM department)
        {
            if (id != department.Id) return View("NotFound");
            if (!ModelState.IsValid) return View(department);
            await _service.UpdateDepartmentAsync(department);
            return RedirectToAction(nameof(Index));
        }

        //Get: Departments/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var departmentDetails = await _service.GetByIdAsync(id);
            if (departmentDetails == null) return View("NotFound");
            return View(departmentDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var departmentDetails = await _service.GetByIdAsync(id);
            if (departmentDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
