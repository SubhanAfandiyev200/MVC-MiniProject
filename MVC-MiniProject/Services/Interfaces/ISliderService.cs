using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderUIVM>> GetAllUIAsync();
        Task<IEnumerable<SliderVM>> GetAllAsync();
        Task<SliderDetailVM> GetDetailAsync(int id);
        Task CreateAsync(SliderCreateVM model);
        Task DeleteAsync(int id);
        Task EditAsync(int id, SliderEditVM model);
        Task<Slider> GetByIdAsync(int id);
    }
}
