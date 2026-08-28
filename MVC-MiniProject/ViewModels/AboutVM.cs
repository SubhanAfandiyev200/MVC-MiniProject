
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.ViewModels
{
    public class AboutVM
    {
        public IEnumerable<TeacherUIVM> Teachers { get; set; }
        public IEnumerable<IconUIVM> Icons { get; set; }
        public AboutPlatformUIVM AboutPlatforms { get; set; }
        public AboutVisionUIVM AboutVisions { get; set; }
    }
}
