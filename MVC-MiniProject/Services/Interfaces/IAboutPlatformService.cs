using MVC_MiniProject.ViewModels.AboutPlatforms;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutPlatformService
    {
        Task<AboutPlatformUIVM> GetUIAsync();
    } 
}
