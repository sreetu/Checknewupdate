using Check1.Models;
using System.ComponentModel.DataAnnotations;

namespace Check1.Data.ViewModels
{
    public class NewCourseVM
    {
        public int Id { get; set; }
        [Display(Name = "Course name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Course description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = " Max Marks")]
        [Required(ErrorMessage = "Max Marks is required")]
        public int MaxMarks { get; set; }
        [Display(Name = "Passing Marks")]
        [Required(ErrorMessage = "Passing Marks is required")]
        public int PassMarks { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
