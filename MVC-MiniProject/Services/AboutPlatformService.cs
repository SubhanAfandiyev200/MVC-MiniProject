using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;

namespace MVC_MiniProject.Services
{
    public class AboutPlatformService : IAboutPlatformService
    {
        private readonly AppDbContext _dbContext;
        public AboutPlatformService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
