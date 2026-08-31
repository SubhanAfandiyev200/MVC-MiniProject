using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsUIVM>> GetAllUIAsync();
        Task<IEnumerable<NewsVM>> GetAllAsync();
        Task<NewsDetailVM> GetDetailAsync(int id);
        Task CreateAsync(NewsCreateVM model);
        Task DeleteAsync(int id);
        Task EditAsync(int id, NewsEditVM model);
        Task<News> GetByIdAsync(int id);
    }
}
