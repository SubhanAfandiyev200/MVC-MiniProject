using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutVisionService
    {
        Task<AboutVisionUIVM> GetUIAsync();
        Task<IEnumerable<AboutVisionVM>> GetAllAsync();
        Task<AboutVisionDetailVM> GetDetailAsync(int id);
        Task CreateAsync(AboutVisionCreateVM model);
        Task DeleteAsync(int id);
        Task EditAsync(int id, AboutVisionEditVM model);
        Task<AboutVision> GetByIdAsync(int id);
    }
}
