using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _dbContext;
        public EventService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EventUIVM>> GetAllUIAsync()
        {
            var events = await _dbContext.Events.Select(m=>new EventUIVM
            {
                DateDay = m.DateDay,
                DateMonth = m.DateMonth,
                Location = m.Location,
                Title = m.Title,
            }).ToListAsync();
            return events;
        }
    }
}
