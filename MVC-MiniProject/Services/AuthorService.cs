using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _dbContext;
        public AuthorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(AuthorCreateVM model)
        {
            var author = await _dbContext.Authors.AddAsync(new Author
            {
                FullName = model.Fullname
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author is null) throw new NotFoundException();
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorVM>> GetAllAsync()
        {
            var authors = await _dbContext.Authors.OrderByDescending(m=>m.Id).Select(m=>new AuthorVM
            {
                Id = m.Id,
                Fullname = m.FullName
            }).ToListAsync();
            return authors;
        }

        public async Task<AuthorDetailVM> GetDetailAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author is null) throw new NotFoundException();
            return new AuthorDetailVM
            {
                Id = author.Id,
                Name = author.FullName
            };
        }
        public async Task EditAsync(int id, AuthorEditVM model)
        {
            var authors = await _dbContext.Authors.FindAsync(id);
            if (authors is null) throw new NotFoundException();
            authors.FullName = model.Fullname;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Author> GetByIdAsync(int id)
        {
            var authors = await _dbContext.Authors.FindAsync(id);
            if (authors is null) throw new NotFoundException();
            return authors;
        }
    }
}
