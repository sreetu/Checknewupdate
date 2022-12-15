using Check1.Data.Services;
using Check1.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Check1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _service;

        public CoursesController(ICoursesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCourses = await _service.GetAllAsync();
            //n => n.Cinema
            return View(allCourses);
        }

        /*[AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allCourses = await _service.GetAllAsync();
            //n => n.Cinema
            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allCourses.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allCourses.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allCourses);
        }
        */
        //GET: Courses/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var CourseDetail = await _service.GetCourseByIdAsync(id);
            return View(CourseDetail);
        }

        //GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var CourseDropdownsData = await _service.GetNewCourseDropdownsValues();
            ViewBag.Students = new SelectList(CourseDropdownsData.Students, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCourseVM Course)
        {
            if (!ModelState.IsValid)
            {
                var CourseDropdownsData = await _service.GetNewCourseDropdownsValues();
                ViewBag.Students = new SelectList(CourseDropdownsData.Students, "Id", "Name");

                return View(Course);
            }

            await _service.AddNewCourseAsync(Course);
            return RedirectToAction(nameof(Index));
        }


        //GET: Courses/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var CourseDetails = await _service.GetCourseByIdAsync(id);
            if (CourseDetails == null) return View("NotFound");

            var response = new NewCourseVM()
            {
                Id = CourseDetails.Id,
                Name = CourseDetails.Name,
                Description = CourseDetails.Description,
                MaxMarks=CourseDetails.MaxMarks,
                PassMarks=CourseDetails.PassMarks,
                StudentIds = CourseDetails.Student_Courses.Select(n => n.StudentId).ToList(),
            };

            var CourseDropdownsData = await _service.GetNewCourseDropdownsValues();
            ViewBag.Students = new SelectList(CourseDropdownsData.Students, "Id", "Name");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewCourseVM Course)
        {
            if (id != Course.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var CourseDropdownsData = await _service.GetNewCourseDropdownsValues();
                ViewBag.Students = new SelectList(CourseDropdownsData.Students, "Id", "Name");

                return View(Course);
            }

            await _service.UpdateCourseAsync(Course);
            return RedirectToAction(nameof(Index));
        }
    }
}
