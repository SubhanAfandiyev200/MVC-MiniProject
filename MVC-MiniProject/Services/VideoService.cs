using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Videos;

namespace MVC_MiniProject.Services
{
    public class VideoService : IVideoService
    {
        private readonly AppDbContext _dbContext;
        public VideoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VideoUIVM> GetUIAsync()
        {
            var videos = await _dbContext.Videos.OrderByDescending(m => m.Id).Select(m=>new VideoUIVM
            {
                Name = m.Name
            }).FirstOrDefaultAsync();
            return videos;
        }
    }
}
