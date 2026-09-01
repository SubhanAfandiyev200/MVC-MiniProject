using MVC_MiniProject.ViewModels.Settings;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllUIAsync();
        Task<List<SettingVM>> GetAllAsync();
    }
}
