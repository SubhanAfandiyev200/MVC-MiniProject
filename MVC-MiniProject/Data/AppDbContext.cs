using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using System.Drawing;

namespace MVC_MiniProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseInfo> CourseInfos { get; set; }
        public DbSet<AboutPlatform> AboutPlatforms { get; set; }
        public DbSet<AboutVision> AboutVision { get; set; }
        public DbSet<Video> Videos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
