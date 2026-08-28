using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.ViewModels
{
    public class CourseVM
    {
        public Dictionary<string, string> Settings { get; set; }
        public IEnumerable<CourseInfoUIVM> CourseInfos { get; set; }
    }
}
