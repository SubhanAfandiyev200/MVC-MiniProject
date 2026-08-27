using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _dbContext;
        public NewsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<NewsUIVM>> GetAllUIAsync()
        {
            var news = await _dbContext.News.Include(m=>m.Authors).Select(m=>new NewsUIVM
            {
                Author = m.Authors.FullName,
                Date = m.Date,
                Image = m.Image,
                Question = m.Question,
            }).ToListAsync();
            return news;
        }
    }
}
