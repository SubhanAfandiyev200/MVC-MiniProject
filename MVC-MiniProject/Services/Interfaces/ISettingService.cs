using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Settings;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllUIAsync();
        Task<List<SettingVM>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
        Task EditAsync(int id, SettingEditVM model);
    }
}
