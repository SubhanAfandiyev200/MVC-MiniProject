using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorVM>> GetAllAsync();
        Task CreateAsync(AuthorCreateVM model);
        Task<AuthorDetailVM> GetDetailAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, AuthorEditVM model);
        Task<Author> GetByIdAsync(int id);
    }
}
