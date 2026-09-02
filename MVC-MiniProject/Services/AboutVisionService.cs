using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;

namespace MVC_MiniProject.Services
{
    public class AboutVisionService : IAboutVisionService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public AboutVisionService(AppDbContext dbContext,
                                    IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        public async Task<AboutVisionUIVM> GetUIAsync()
        {
            var aboutVision = await _dbContext.AboutVision.OrderByDescending(m => m.Id).Select(m => new AboutVisionUIVM
            {
                Description = m.Description,
                Image = m.Image,
                Title = m.Title
            }).FirstOrDefaultAsync();
            return aboutVision;
        }
        public async Task CreateAsync(AboutVisionCreateVM model)
        {
            string fileName = await _fileService.UploadFileAsync(model.Image, "images");
            await _dbContext.AboutVision.AddAsync(new AboutVision
            {
                Description = model.Description,
                Title = model.Title,
                Image = fileName
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AboutVisionVM>> GetAllAsync()
        {
            var aboutVisions = await _dbContext.AboutVision
                .OrderByDescending(m => m.Id)
                .Select(m => new AboutVisionVM
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Image = m.Image
                })
                .ToListAsync();

            return aboutVisions;
        }

        public async Task<AboutVisionDetailVM> GetDetailAsync(int id)
        {
            var vision = await _dbContext.AboutVision.FindAsync(id);
            if (vision is null) throw new NotFoundException();
            return new AboutVisionDetailVM
            {
                Title = vision.Title,
                Description = vision.Description,
                Image = vision.Image
            };
        }



        public async Task DeleteAsync(int id)
        {
            var vision = await _dbContext.AboutVision.FindAsync(id);
            if (vision is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(vision.Image, "images");
            _dbContext.AboutVision.Remove(vision);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditAsync(int id, AboutVisionEditVM model)
        {
            var vision = await _dbContext.AboutVision.FindAsync(id);
            if (vision is null) throw new NotFoundException();

            vision.Image = model.ExistingImage;
            vision.Description = model.Description;
            vision.Title = model.Title;

            if (model.Image is not null)
            {
                await _fileService.DeleteFileAsync(vision.Image, "images");
                vision.Image = await _fileService.UploadFileAsync(model.Image, "images");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<AboutVision> GetByIdAsync(int id)
        {
            var vision = await _dbContext.AboutVision.FindAsync(id);
            if (vision is null) throw new NotFoundException();
            return vision;
        }
    }
}
