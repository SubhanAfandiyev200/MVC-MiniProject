using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _dbContext;
        public SliderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
