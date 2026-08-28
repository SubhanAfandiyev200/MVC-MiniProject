using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ICourseInfoService
    {
        public Task<IEnumerable<CourseInfoUIVM>> GetAllUIAsync();
        public Task<CourseDetailUIVM> GetDetailUIAsync(int id);
    }
}
