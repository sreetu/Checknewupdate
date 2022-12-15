using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;

namespace Check1.Data.Services
{
    public interface ICoursesService:IEntityBaseRepository<Course>
    {
        Task<Course> GetCourseByIdAsync(int id);
        Task<NewCourseDropdownsVM> GetNewCourseDropdownsValues();
        Task AddNewCourseAsync(NewCourseVM data);
        Task UpdateCourseAsync(NewCourseVM data);
    }
}
