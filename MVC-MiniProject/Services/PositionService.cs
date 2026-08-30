using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Positions;

namespace MVC_MiniProject.Services
{
    public class PositionService : IPositionService
    {
        private readonly AppDbContext _dbContext;
        public PositionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(PositionCreateVM model)
        {
            var position = await _dbContext.Positions.AddAsync(new Position
            {
                Name = model.Name
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await _dbContext.Positions.FindAsync(id);
            if (position is null) throw new NotFoundException();
            _dbContext.Positions.Remove(position);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _dbContext.Positions.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<PositionVM>> GetAllAsync()
        {
            var positions = await _dbContext.Positions.OrderByDescending(m=>m.Id).Select(m=>new PositionVM
            {
                Id = m.Id,
                Name = m.Name
            }).ToListAsync();
            return positions;
        }

        public async Task<PositionDetailVM> GetDetailAsync(int id)
        {
            var position = await _dbContext.Positions.FindAsync(id);
            if (position is null) throw new NotFoundException();
            return new PositionDetailVM
            {
                Name = position.Name
            };
        }
        public async Task EditAsync(int id, PositionEditVM model)
        {
            var position = await _dbContext.Positions.FindAsync(id);
            if (position is null) throw new NotFoundException();
            position.Name = model.Name;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Position> GetByIdAsync(int id)
        {
            var position = await _dbContext.Positions.FindAsync(id);
            if (position is null) throw new NotFoundException();
            return position;
        }
    }
}
