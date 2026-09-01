using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.News;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherUIVM>> GetAllUIAsync();
        Task<IEnumerable<TeacherVM>> GetAllAsync();
        Task<TeacherDetailVM> GetDetailAsync(int id);
        Task CreateAsync(TeacherCreateVM model);
        Task EditAsync(int id, TeacherEditVM model);
        Task<Teacher> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
