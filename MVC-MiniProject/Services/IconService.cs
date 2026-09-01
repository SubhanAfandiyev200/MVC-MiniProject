using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services
{
    public class IconService : IIconService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public IconService(AppDbContext dbContext,
                           IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task CreateAsync(IconCreateVM model)
        {
            string fileName = await _fileService.UploadFileAsync(model.Name, "images");
            var icon = await _dbContext.Icons.AddAsync(new Icon
            {
                Name = fileName
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var icon = await _dbContext.Icons.FindAsync(id);
            if (icon is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(icon.Name, "images");
            _dbContext.Icons.Remove(icon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IconVM>> GetAllAsync()
        {
            var icons = await _dbContext.Icons.OrderByDescending(m=>m.Id).Select(m => new IconVM
            {
                Id = m.Id,
                Name = m.Name
            }).ToListAsync();
            return icons;
        }

        public async Task<IEnumerable<IconUIVM>> GetAllUIAsync()
        {
            var icons = await _dbContext.Icons.Select(m => new IconUIVM
            {
                Name = m.Name
            }).ToListAsync();
            return icons;
        }

        public async Task<IconDetailVM> GetDetailAsync(int id)
        {
            var icon = await _dbContext.Icons.FindAsync(id);
            if (icon is null) throw new NotFoundException();
            return new IconDetailVM
            {
                Name = icon.Name
            };
        }
        public async Task EditAsync(int id, IconEditVM model)
        {
            var icon = await _dbContext.Icons.FindAsync(id);
            if (icon is null) throw new NotFoundException();

            icon.Name = model.ExistingImage;

            if (model.Name is not null)
            {
                await _fileService.DeleteFileAsync(icon.Name, "images");
                icon.Name = await _fileService.UploadFileAsync(model.Name, "images");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Icon> GetByIdAsync(int id)
        {
            var icon = await _dbContext.Icons.FindAsync(id);
            if (icon is null)
            {
                throw new NotFoundException();
            }
            return icon;
        }

    }
}
