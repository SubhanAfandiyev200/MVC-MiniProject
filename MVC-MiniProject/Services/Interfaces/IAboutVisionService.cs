using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAboutVisionService
    {
        Task<AboutVisionUIVM> GetUIAsync();
    }
}
