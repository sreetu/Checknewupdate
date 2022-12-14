using Check1.Data.Base;
using Check1.Data.ViewModels;
using Check1.Models;

namespace Check1.Data.Services
{
    public interface IStudentsService:IEntityBaseRepository<Student>
    {
        Task<Student> GetStudentByIdAsync(int id);
        Task<NewStudentDropdownsVM> GetNewStudentDropdownsValues();
        Task AddNewStudentAsync(NewStudentVM data);
        Task UpdateStudentAsync(NewStudentVM data);
    }
}
