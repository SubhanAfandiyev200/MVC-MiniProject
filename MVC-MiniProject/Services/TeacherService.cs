using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _dbContext;
        public TeacherService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
