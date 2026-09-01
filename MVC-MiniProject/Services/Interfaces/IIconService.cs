using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IIconService
    {
        Task<IEnumerable<IconUIVM>> GetAllUIAsync();
        Task<IEnumerable<IconVM>> GetAllAsync();
        Task<IconDetailVM> GetDetailAsync(int id);
        Task CreateAsync(IconCreateVM model);
        Task DeleteAsync(int id);
        Task EditAsync(int id, IconEditVM model);
        Task<Icon> GetByIdAsync(int id);

    }
}
