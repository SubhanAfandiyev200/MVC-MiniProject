using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Settings;

namespace MVC_MiniProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public SettingService(AppDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
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

        public async Task<Setting> GetByIdAsync(int id)
        {
            var setting = await _dbContext.Settings.FindAsync(id);
            if (setting is null) throw new NotFoundException();
            return setting;
        }

        public async Task EditAsync(int id, SettingEditVM model)
        {
            var setting = await _dbContext.Settings.FindAsync(id);
            if (setting is null) throw new NotFoundException();

            if (setting.Key.Equals("logo", StringComparison.OrdinalIgnoreCase))
            {
                if (model.Image is not null)
                {
                    await _fileService.DeleteFileAsync(setting.Value, "images");
                    setting.Value = await _fileService.UploadFileAsync(model.Image, "images");
                }
            }
            else
            {
                setting.Value = model.Value;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
