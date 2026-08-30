using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Positions;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionVM>> GetAllAsync();
        Task CreateAsync(PositionCreateVM model);
        Task<bool> ExistAsync(string name);
        Task DeleteAsync(int id);
        Task<PositionDetailVM> GetDetailAsync(int id);
        Task EditAsync(int id, PositionEditVM model);
        Task<Position> GetByIdAsync(int id);
    }
}
