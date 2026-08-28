using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;

namespace MVC_MiniProject.Services
{
    public class AboutVisionService : IAboutVisionService
    {
        private readonly AppDbContext _dbContext;
        public AboutVisionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AboutVisionUIVM> GetUIAsync()
        {
            var aboutVision = await _dbContext.AboutPlatforms.OrderByDescending(m => m.Id).Select(m => new AboutVisionUIVM
            {
                Description = m.Description,
                Image = m.Image,
                Title = m.Title
            }).FirstOrDefaultAsync();
            return aboutVision;
        }
    }
}
