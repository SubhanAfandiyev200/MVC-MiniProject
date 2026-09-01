using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public SliderService(AppDbContext dbContext,
                             IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task CreateAsync(SliderCreateVM model)
        {
            string fileLogo = await _fileService.UploadFileAsync(model.Logo, "images");
            string fileImage = await _fileService.UploadFileAsync(model.Image, "images");
            var slider = await _dbContext.Sliders.AddAsync(new Slider
            {
                Description = model.Description,
                Title = model.Title,
                Image = fileImage,
                Logo = fileLogo
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _dbContext.Sliders.FindAsync(id);
            if (slider is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(slider.Logo, "images");
            await _fileService.DeleteFileAsync(slider.Image, "images");
            _dbContext.Sliders.Remove(slider);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SliderVM>> GetAllAsync()
        {
            var sliders = await _dbContext.Sliders.OrderByDescending(m=>m.Id).Select(m => new SliderVM
            {
                Description = m.Description,
                Id = m.Id,
                Image = m.Image,
                Logo = m.Logo,
                Title = m.Title
            }).ToListAsync();
            return sliders;
        }

        public async Task<IEnumerable<SliderUIVM>> GetAllUIAsync()
        {
            var sliders = await _dbContext.Sliders.Select(m=>new SliderUIVM
            {
                Description = m.Description,
                Image = m.Image,
                Logo = m.Logo,
                Title = m.Title
            }).ToListAsync();
            return sliders;
        }

        public async Task<SliderDetailVM> GetDetailAsync(int id)
        {
            var slider = await _dbContext.Sliders.FindAsync(id);
            if (slider is null) throw new NotFoundException();
            return new SliderDetailVM
            {
                Title = slider.Title,
                Logo = slider.Logo,
                Image = slider.Image,
                Description = slider.Description
            };
        }
        public async Task EditAsync(int id, SliderEditVM model)
        {
            var slider = await _dbContext.Sliders.FindAsync(id);
            if (slider is null) throw new NotFoundException();

            slider.Image = model.ExistingImage;
            slider.Logo = model.ExistingLogo;
            slider.Title = model.Title;
            slider.Description = model.Description;

            if (model.Image is not null)
            {
                await _fileService.DeleteFileAsync(slider.Image, "images");
                slider.Image = await _fileService.UploadFileAsync(model.Image, "images");
            }
            if (model.Logo is not null)
            {
                await _fileService.DeleteFileAsync(slider.Logo, "images");
                slider.Logo = await _fileService.UploadFileAsync(model.Logo, "images");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            var slider = await _dbContext.Sliders.FindAsync(id);
            if (slider is null)
            {
                throw new NotFoundException();
            }
            return slider;
        }
    }
}
