using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.CourseInfos;
using MVC_MiniProject.ViewModels.Events;
using MVC_MiniProject.ViewModels.Icons;
using MVC_MiniProject.ViewModels.News;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<IconUIVM> Icons { get; set; }
        public IEnumerable<SliderUIVM> Sliders { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public IEnumerable<EventUIVM> Events { get; set; }
        public IEnumerable<NewsUIVM> News { get; set; }
        public IEnumerable<CourseInfoUIVM> CourseInfos { get; set; }
    }
}
