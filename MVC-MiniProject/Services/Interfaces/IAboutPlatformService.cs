using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutPlatformService
    {
        Task<AboutPlatformUIVM> GetUIAsync();
        Task<IEnumerable<AboutPlatformVM>> GetAllAsync();
        Task<AboutPlatformDetailVM> GetDetailAsync(int id);
        Task CreateAsync(AboutPlatformCreateVM model);
        Task DeleteAsync(int id);
        Task EditAsync(int id, AboutPlatformEditVM model);
        Task<AboutPlatform> GetByIdAsync(int id);
    } 
}
