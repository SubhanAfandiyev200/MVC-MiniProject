using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services
{
    public class AboutPlatformService : IAboutPlatformService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public AboutPlatformService(AppDbContext dbContext,
                                    IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task CreateAsync(AboutPlatformCreateVM model)
        {
            string fileName = await _fileService.UploadFileAsync(model.Image, "images");
            await _dbContext.AboutPlatforms.AddAsync(new AboutPlatform
            {
                Description = model.Description,
                Title = model.Title,
                Image = fileName
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AboutPlatformVM>> GetAllAsync()
        {
            var aboutPlatforms = await _dbContext.AboutPlatforms
                .OrderByDescending(m => m.Id)
                .Select(m => new AboutPlatformVM
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Image = m.Image
                })
                .ToListAsync();

            return aboutPlatforms;
        }

        public async Task<AboutPlatformDetailVM> GetDetailAsync(int id)
        {
            var platform = await _dbContext.AboutPlatforms.FindAsync(id);
            if (platform is null) throw new NotFoundException();
            return new AboutPlatformDetailVM
            {
                Title = platform.Title,
                Description = platform.Description,
                Image = platform.Image
            };
        }

        public async Task<AboutPlatformUIVM> GetUIAsync()
        {
            var aboutPlatform = await _dbContext.AboutPlatforms.OrderByDescending(m=>m.Id).Select(m=>new AboutPlatformUIVM
            {
                Description = m.Description,
                Image = m.Image,
                Title = m.Title
            }).FirstOrDefaultAsync();
            return aboutPlatform;
        }


        public async Task DeleteAsync(int id)
        {
            var platform = await _dbContext.AboutPlatforms.FindAsync(id);
            if (platform is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(platform.Image, "images");
            _dbContext.AboutPlatforms.Remove(platform);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditAsync(int id, AboutPlatformEditVM model)
        {
            var platform = await _dbContext.AboutPlatforms.FindAsync(id);
            if (platform is null) throw new NotFoundException();

            platform.Image = model.ExistingImage;
            platform.Description = model.Description;
            platform.Title = model.Title;

            if (model.Image is not null)
            {
                await _fileService.DeleteFileAsync(platform.Image, "images");
                platform.Image = await _fileService.UploadFileAsync(model.Image, "images");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<AboutPlatform> GetByIdAsync(int id)
        {
            var platform = await _dbContext.AboutPlatforms.FindAsync(id);
            if (platform is null) throw new NotFoundException();
            return platform;
        }
    }
}
