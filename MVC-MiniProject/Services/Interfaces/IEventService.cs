using MVC_MiniProject.Data;
using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventUIVM>> GetAllUIAsync();
        Task<IEnumerable<EventVM>> GetAllAsync();
        Task CreateAsync(EventCreateVM model);
        Task<EventDetailVM> GetDetailAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, EventEditVM model);
        Task<Event> GetByIdAsync(int id);
    }
}
