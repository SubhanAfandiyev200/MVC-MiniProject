using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Settings;

namespace MVC_MiniProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _dbContext;
        public SettingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SettingVM>> GetAllAsync()
        {
            var settings = await _dbContext.Settings.OrderBy(s => s.Id).Select(s => new SettingVM
                {
                    Id = s.Id,
                    Key = s.Key,
                    Value = s.Value
                })
                .ToListAsync();

            return settings;
        }

        public async Task<Dictionary<string, string>> GetAllUIAsync()
        {
            var settings = await _dbContext.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
            return settings;
        }
    }
}
