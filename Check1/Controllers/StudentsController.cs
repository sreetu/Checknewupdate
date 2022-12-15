using Check1.Data.Services;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Check1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _service;

        public StudentsController(IStudentsService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            var allStudents = await _service.GetAllAsync();
            //n => n.Course
            return View(allStudents);
        }

        /*
        public async Task<IActionResult> Filter(string searchString)
        {
            var allStudents = await _service.GetAllAsync(n => n.Department);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allStudents.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allStudents.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allStudents);
        }*/

        //GET: Students/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var StudentDetail = await _service.GetStudentByIdAsync(id);
            return View(StudentDetail);
        }

        //GET: Students/Create
        public async Task<IActionResult> Create()
        {
            var StudentDropdownsData = await _service.GetNewStudentDropdownsValues();

            ViewBag.Courses = new SelectList(StudentDropdownsData.Courses, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewStudentVM Student)
        {
            if (!ModelState.IsValid)
            {
                var StudentDropdownsData = await _service.GetNewStudentDropdownsValues();

                ViewBag.Courses = new SelectList(StudentDropdownsData.Courses, "Id", "Name");
                return View(Student);
            }

            await _service.AddNewStudentAsync(Student);
            return RedirectToAction(nameof(Index));
        }


        //GET: Students/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var StudentDetails = await _service.GetStudentByIdAsync(id);
            if (StudentDetails == null) return View("NotFound");

            var response = new NewStudentVM()
            {
                Id = StudentDetails.Id,
                Name = StudentDetails.Name,
                CourseIds = StudentDetails.Student_Courses.Select(n => n.CourseId).ToList(),
            };

            var StudentDropdownsData = await _service.GetNewStudentDropdownsValues();
            ViewBag.Courses = new SelectList(StudentDropdownsData.Courses, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewStudentVM Student)
        {
            if (id != Student.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var StudentDropdownsData = await _service.GetNewStudentDropdownsValues();

                ViewBag.Courses = new SelectList(StudentDropdownsData.Courses, "Id", "Name");
                return View(Student);
            }

            await _service.UpdateStudentAsync(Student);
            return RedirectToAction(nameof(Index));
        }
    }
}
