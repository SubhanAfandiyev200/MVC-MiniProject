using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ICourseInfoService
    {
        // UI Methods
        Task<IEnumerable<CourseInfoUIVM>> GetAllUIAsync();
        Task<CourseDetailUIVM> GetDetailUIAsync(int id);
        Task<IEnumerable<SearchCourseUIVM>> GetSearchedCourseUIAsync(string searchText);

        // Admin Methods
        Task<IEnumerable<CourseInfoVM>> GetAllAsync();
        Task<CourseInfoDetailVM> GetDetailAsync(int id);
        Task CreateAsync(CourseInfoCreateVM model);
        Task EditAsync(int id, CourseInfoEditVM model);
        Task DeleteAsync(int id);
        Task<CourseInfo> GetByIdAsync(int id);
        Task<CourseInfoEditVM> GetEditVMAsync(int id);
        Task PopulateEditVMAsync(CourseInfoEditVM model, int id);
        Task UpdateMainImageAsync(int courseInfoId, int imageId);
        Task DeleteImageAsync(int imageId);
    }
}
