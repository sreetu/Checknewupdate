using Check1.Data.Services;
using Check1.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Check1.Controllers
{
    public class StudentAttendancesController : Controller
    {
        private readonly IStudentAttendancesService _service;

        public StudentAttendancesController(IStudentAttendancesService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            var allStudentAttendances = await _service.GetAllAsync(n => n.Student);
            return View(allStudentAttendances);
        }


        
        //GET: StudentAttendances/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var StudentAttendanceDetail = await _service.GetStudentAttendanceByIdAsync(id);
            return View(StudentAttendanceDetail);
        }

        //GET: StudentAttendances/Create
        public async Task<IActionResult> Create()
        {
            var StudentAttendanceDropdownsData = await _service.GetNewStudentAttendanceDropdownsValues();

            ViewBag.Students = new SelectList(StudentAttendanceDropdownsData.Students, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewStudentAttendanceVM StudentAttendance)
        {
            if (!ModelState.IsValid)
            {
                var StudentAttendanceDropdownsData = await _service.GetNewStudentAttendanceDropdownsValues();

                ViewBag.Students = new SelectList(StudentAttendanceDropdownsData.Students, "Id", "Name");
                return View(StudentAttendance);
            }

            await _service.AddNewStudentAttendanceAsync(StudentAttendance);
            return RedirectToAction(nameof(Index));
        }


        //GET: StudentAttendances/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var StudentAttendanceDetails = await _service.GetStudentAttendanceByIdAsync(id);
            if (StudentAttendanceDetails == null) return View("NotFound");

            var response = new NewStudentAttendanceVM()
            {
                Id = StudentAttendanceDetails.Id,
                CurrDate = StudentAttendanceDetails.CurrDate,
                AttendanceStatus = StudentAttendanceDetails.AttendanceStatus,
                StudentId = StudentAttendanceDetails.StudentId,
            };

            var StudentAttendanceDropdownsData = await _service.GetNewStudentAttendanceDropdownsValues();
            ViewBag.Students = new SelectList(StudentAttendanceDropdownsData.Students, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewStudentAttendanceVM StudentAttendance)
        {
            if (id != StudentAttendance.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var StudentAttendanceDropdownsData = await _service.GetNewStudentAttendanceDropdownsValues();

                ViewBag.Students = new SelectList(StudentAttendanceDropdownsData.Students, "Id", "Name");
                return View(StudentAttendance);
            }

            await _service.UpdateStudentAttendanceAsync(StudentAttendance);
            return RedirectToAction(nameof(Index));
        }
    }
}
