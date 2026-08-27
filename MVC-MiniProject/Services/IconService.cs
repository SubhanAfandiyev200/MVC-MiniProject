using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Icons;

namespace MVC_MiniProject.Services
{
    public class IconService : IIconService
    {
        private readonly AppDbContext _dbContext;
        public IconService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IconUIVM>> GetAllUIAsync()
        {
            var icons = await _dbContext.Icons.Select(m => new IconUIVM
            {
                Name = m.Name
            }).ToListAsync();
            return icons;
        }
    }
}
