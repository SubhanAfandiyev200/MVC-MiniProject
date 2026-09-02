using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.Services
{
    public class CourseInfoService : ICourseInfoService
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;

        public CourseInfoService(AppDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
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

        public async Task<IEnumerable<SearchCourseUIVM>> GetSearchedCourseUIAsync(string searchText)
        {
            searchText ??= "";
            var courses = await _dbContext.CourseInfos.Include(m => m.Teacher)
                                                      .Include(m => m.CourseImages)
                                                      .Where(m => m.Title.Contains(searchText)).Select(m=>new SearchCourseUIVM
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
            return courses;
        }

        // Admin Methods
        public async Task<IEnumerable<CourseInfoVM>> GetAllAsync()
        {
            return await _dbContext.CourseInfos
                .Include(m => m.Teacher)
                .Include(m => m.CourseImages)
                .OrderByDescending(m => m.Id)
                .Select(m => new CourseInfoVM
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Price = m.Price,
                    SalesCount = m.SalesCount,
                    IsFeature = m.IsFeature,
                    IsNew = m.IsNew,
                    Teacher = m.Teacher.FullName,
                    ImageCount = m.CourseImages.Count,
                    MainImage = m.CourseImages.FirstOrDefault(img => img.IsMain).Name
                }).ToListAsync();
        }

        public async Task<CourseInfoDetailVM> GetDetailAsync(int id)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.Teacher)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();

            return new CourseInfoDetailVM
            {
                Id = courseInfo.Id,
                Title = courseInfo.Title,
                Description = courseInfo.Description,
                Price = courseInfo.Price,
                SalesCount = courseInfo.SalesCount,
                IsFeature = courseInfo.IsFeature,
                IsNew = courseInfo.IsNew,
                Teacher = courseInfo.Teacher.FullName,
                TeacherId = courseInfo.TeacherId,
                Images = courseInfo.CourseImages.Select(img => new CourseImageVM
                {
                    Id = img.Id,
                    Name = img.Name,
                    IsMain = img.IsMain
                }).ToList()
            };
        }

        public async Task CreateAsync(CourseInfoCreateVM model)
        {
            var courseInfo = new CourseInfo
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                SalesCount = model.SalesCount,
                IsFeature = model.IsFeature,
                IsNew = model.IsNew,
                TeacherId = int.Parse(model.TeacherId),
                CourseImages = new List<CourseImage>()
            };

            // İlk şəkli IsMain = true olaraq əlavə et
            for (int i = 0; i < model.Images.Count; i++)
            {
                string fileName = await _fileService.UploadFileAsync(model.Images[i], "images");
                courseInfo.CourseImages.Add(new CourseImage
                {
                    Name = fileName,
                    IsMain = i == 0 // İlk şəkil əsas olur
                });
            }

            await _dbContext.CourseInfos.AddAsync(courseInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CourseInfoEditVM model)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();

            var teacherExists = await _dbContext.Teachers.AnyAsync(t => t.Id == model.TeacherId);
            if (!teacherExists) throw new NotFoundException();

            courseInfo.Title = model.Title;
            courseInfo.Description = model.Description;
            courseInfo.Price = model.Price;
            courseInfo.SalesCount = model.SalesCount;
            courseInfo.IsFeature = model.IsFeature;
            courseInfo.IsNew = model.IsNew;
            courseInfo.TeacherId = model.TeacherId;

            // Yeni şəkillər əlavə et
            if (model.NewImages != null && model.NewImages.Any())
            {
                foreach (var image in model.NewImages)
                {
                    string fileName = await _fileService.UploadFileAsync(image, "images");
                    courseInfo.CourseImages.Add(new CourseImage
                    {
                        Name = fileName,
                        IsMain = false // Yeni şəkillər default olaraq IsMain = false
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();

            // Bütün şəkilləri sil
            foreach (var image in courseInfo.CourseImages)
            {
                await _fileService.DeleteFileAsync(image.Name, "images");
            }

            _dbContext.CourseInfos.Remove(courseInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CourseInfo> GetByIdAsync(int id)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.Teacher)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();
            return courseInfo;
        }

        public async Task<CourseInfoEditVM> GetEditVMAsync(int id)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();

            return new CourseInfoEditVM
            {
                Title = courseInfo.Title,
                Description = courseInfo.Description,
                Price = courseInfo.Price,
                SalesCount = courseInfo.SalesCount,
                IsFeature = courseInfo.IsFeature,
                IsNew = courseInfo.IsNew,
                TeacherId = courseInfo.TeacherId,
                ExistingImages = courseInfo.CourseImages.Select(img => new CourseImageVM
                {
                    Id = img.Id,
                    Name = img.Name,
                    IsMain = img.IsMain
                }).ToList()
            };
        }

        public async Task PopulateEditVMAsync(CourseInfoEditVM model, int id)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseInfo is null) throw new NotFoundException();

            model.ExistingImages = courseInfo.CourseImages.Select(img => new CourseImageVM
            {
                Id = img.Id,
                Name = img.Name,
                IsMain = img.IsMain
            }).ToList();
        }

        public async Task UpdateMainImageAsync(int courseInfoId, int imageId)
        {
            var courseInfo = await _dbContext.CourseInfos
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync(m => m.Id == courseInfoId);

            if (courseInfo is null) throw new NotFoundException();

            var targetImage = courseInfo.CourseImages.FirstOrDefault(img => img.Id == imageId);
            if (targetImage is null) throw new NotFoundException();

            // Bütün şəkillərin IsMain-ini false et
            foreach (var image in courseInfo.CourseImages)
            {
                image.IsMain = false;
            }

            // Seçilən şəkli IsMain = true et
            targetImage.IsMain = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _dbContext.CourseImages.FindAsync(imageId);
            if (image is null) throw new NotFoundException();

            // Əgər bu şəkil IsMain idisə, silməyə icazə vermə
            if (image.IsMain)
            {
                throw new InvalidOperationException("Cannot delete main image. Please set another image as main first.");
            }

            await _fileService.DeleteFileAsync(image.Name, "images");
            _dbContext.CourseImages.Remove(image);
            await _dbContext.SaveChangesAsync();
        }
    }
}
