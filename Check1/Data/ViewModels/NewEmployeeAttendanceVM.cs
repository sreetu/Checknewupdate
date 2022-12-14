using Check1.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Check1.Data.ViewModels
{
    public class NewEmployeeAttendanceVM
    {
        public int Id { get; set; }

        [Display(Name = "Curr Date")]
        [Required(ErrorMessage = "Date is required")]
        public string CurrDate { get; set; }
        [Display(Name = "Attendance Status")]
        [Required(ErrorMessage = "Status is required")]
        public AttendanceStatus? AttendanceStatus { get; set; }

        //Relationships


        [Display(Name = "Select an Employee")]
        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeId { get; set; }
    }
}
