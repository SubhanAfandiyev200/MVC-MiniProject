using MVC_MiniProject.ViewModels.Events;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.ViewModels.Selections
{
    public class SelectionVM
    {
        public IEnumerable<NewsUIVM> News { get; set; }
        public IEnumerable<EventUIVM> Events { get; set; }
    }
}
