using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.Services
{
    public class CourseInfoService : ICourseInfoService
    {
        private readonly AppDbContext _dbContext;
        public CourseInfoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CourseInfoUIVM>> GetAllUIAsync()
        {
            var courseInfos = await _dbContext.CourseInfos.Include(m=>m.Teacher).Include(m=>m.CourseImages).Select(m=>new CourseInfoUIVM
            {
                Id = m.Id,
                Description = m.Description,
                Price = m.Price,
                SalesCount = m.SalesCount,
                Title = m.Title,
                TeacherName = m.Teacher.FullName,
                MainImage = m.CourseImages.FirstOrDefault(m => m.IsMain).Name,
                TeacherImage = m.Teacher.Image,
                IsFeature = m.IsFeature,
                IsNew = m.IsNew
            }).ToListAsync();
            return courseInfos;
        }

        public async Task<CourseDetailUIVM> GetDetailUIAsync(int id)
        {
            var course = await _dbContext.CourseInfos.Include(m=>m.Teacher).Include(m=>m.CourseImages).FirstOrDefaultAsync(m => m.Id == id);
            if (course is null) throw new NotFoundException();
            return new CourseDetailUIVM
            {
                Id = course.Id,
                Description = course.Description,
                IsFeature = course.IsFeature,
                IsNew = course.IsNew,
                Title = course.Title,
                SalesCount = course.SalesCount,
                Price = course.Price,
                TeacherImage = course.Teacher.Image,
                TeacherName = course.Teacher.FullName,
                Images = course.CourseImages.Select(m => new CourseImageUIVM
                {
                    IsMain = m.IsMain,
                    Name = m.Name
                }).ToArray()
            };
        }
    }
}
