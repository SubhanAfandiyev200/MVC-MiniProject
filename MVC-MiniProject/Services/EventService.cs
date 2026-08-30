using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
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

        public async Task CreateAsync(EventCreateVM model)
        {
            var events = await _dbContext.Events.AddAsync(new Event
            {
                Title = model.Title,
                Location = model.Location,
                DateMonth = model.DateMonth,
                DateDay = model.DateDay
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedEvent = await _dbContext.Events.FindAsync(id);
            if (deletedEvent is null) throw new NotFoundException();
            _dbContext.Events.Remove(deletedEvent);
            await _dbContext.SaveChangesAsync();

        }

        public async Task EditAsync(int id, EventEditVM model)
        {
            var events = await _dbContext.Events.FindAsync(id);
            if (events is null) throw new NotFoundException();
            events.Title = model.Title;
            events.Location = model.Location;
            events.DateMonth = model.DateMonth;
            events.DateDay = model.DateDay;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventVM>> GetAllAsync()
        {
            return await _dbContext.Events.OrderByDescending(m=>m.Id).Select(m=>new EventVM
            {
                Id = m.Id,
                DateDay = m.DateDay,
                DateMonth = m.DateMonth,
                Location = m.Location,
                Title = m.Title
            }).ToListAsync();
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

        public async Task<Event> GetByIdAsync(int id)
        {
            var events = await _dbContext.Events.FindAsync(id);
            if (events is null) throw new NotFoundException();
            return events;
        }

        public async Task<EventDetailVM> GetDetailAsync(int id)
        {
            var events = await _dbContext.Events.FindAsync(id);
            if (events is null) throw new NotFoundException();
            return new EventDetailVM
            {
                DateDay = events.DateDay,
                DateMonth = events.DateMonth,
                Location = events.Location,
                Title = events.Title,
            };
        }
    }
}
