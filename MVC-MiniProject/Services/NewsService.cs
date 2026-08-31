using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public NewsService(AppDbContext dbContext,
                           IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task CreateAsync(NewsCreateVM model)
        {
            string fileName = await _fileService.UploadFileAsync(model.Image, "images");
            await _dbContext.AddAsync(new News
            {
                Date = model.Date,
                Question = model.Question,
                Image = fileName,
                AuthorId = int.Parse(model.AuthorId)
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var news = await _dbContext.News.FindAsync(id);
            if (news is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(news.Image, "images");
            _dbContext.News.Remove(news);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsVM>> GetAllAsync()
        {
            return await _dbContext.News.OrderByDescending(m=>m.Id).Select(m=>new NewsVM
            {
                Id = m.Id,
                Author = m.Authors.FullName,
                Date = m.Date,
                Image = m.Image,
                Question = m.Question,
            }).ToListAsync();
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

        public async Task<NewsDetailVM> GetDetailAsync(int id)
        {
            var news = await _dbContext.News
                .Include(m => m.Authors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news is null) throw new NotFoundException();
            return new NewsDetailVM
            {
                AuthorId = news.Authors.Id,
                Author = news.Authors.FullName,
                Question = news.Question,
                Image = news.Image,
                Date = news.Date
            };
        }



        public async Task EditAsync(int id, NewsEditVM model)
        {
            var news = await _dbContext.News.FindAsync(id);
            if (news is null) throw new NotFoundException();

            var authorExists = await _dbContext.Authors.AnyAsync(a => a.Id == model.AuthorId);
            if (!authorExists) throw new NotFoundException();
            news.Question = model.Question;
            news.AuthorId = model.AuthorId;
            news.Date = model.Date;
            if (model.Image is not null)
            {
                await _fileService.DeleteFileAsync(news.Image, "images");
                news.Image = await _fileService.UploadFileAsync(model.Image, "images");
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            var news = await _dbContext.News.FindAsync(id);
            if (news is null)
            {
                throw new NotFoundException();
            }
            return news;
        }

    }
}
