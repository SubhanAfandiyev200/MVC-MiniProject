using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;
using MVC_MiniProject.ViewModels.Sliders;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        public TeacherService(AppDbContext dbContext,
                           IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IEnumerable<TeacherVM>> GetAllAsync()
        {
            var teachers = await _dbContext.Teachers.OrderByDescending(m=>m.Id).Include(m=>m.Position).Select(m => new TeacherVM
            {
                Id = m.Id,
                FullName = m.FullName,
                Image = m.Image,
                Position = m.Position.Name
            }).ToListAsync();
            return teachers;
        }

        public async Task<IEnumerable<TeacherUIVM>> GetAllUIAsync()
        {
            var teachers = await _dbContext.Teachers.Include(m => m.Position).Select(m => new TeacherUIVM
            {
                FullName = m.FullName,
                Image = m.Image,
                Position = m.Position.Name
            }).ToListAsync();
            return teachers;
        }
        public async Task<TeacherDetailVM> GetDetailAsync(int id)
        {
            var teacher = await _dbContext.Teachers.Include(m=>m.Position).FirstOrDefaultAsync(m=>m.Id == id);
            if (teacher is null) throw new NotFoundException();
            return new TeacherDetailVM
            {
                FullName = teacher.FullName,
                Position = teacher.Position.Name,
                Image = teacher.Image
            };
        }
        public async Task CreateAsync(TeacherCreateVM model)
        {
            string fileName = await _fileService.UploadFileAsync(model.Image, "images");
            await _dbContext.AddAsync(new Teacher
            {
                FullName = model.Fullname,
                Image = fileName,
                PositionId = int.Parse(model.PositionId)
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher is null) throw new NotFoundException();
            await _fileService.DeleteFileAsync(teacher.Image, "images");
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditAsync(int id, TeacherEditVM model)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher is null) throw new NotFoundException();

            var positionExists = await _dbContext.Positions.AnyAsync(a => a.Id == model.PositionId);
            if (!positionExists) throw new NotFoundException();
            teacher.FullName = model.Fullname;
            teacher.PositionId = model.PositionId;
            if (model.Image is not null)
            {
                await _fileService.DeleteFileAsync(teacher.Image, "images");
                teacher.Image = await _fileService.UploadFileAsync(model.Image, "images");
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher is null)
            {
                throw new NotFoundException();
            }
            return teacher;
        }
    }
}
