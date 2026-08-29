using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Selections;

namespace MVC_MiniProject.ViewComponents
{
    public class SelectionsViewComponent : ViewComponent
    {
        private readonly INewsService _newsService;
        private readonly IEventService _eventService;
        public SelectionsViewComponent(INewsService newsService,
                                       IEventService eventService)
        {
            _newsService = newsService;
            _eventService = eventService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var news = await _newsService.GetAllUIAsync();
            var events = await _eventService.GetAllUIAsync();
            return View(new SelectionVM
            {
                News = news,
                Events = events
            });
        }
    }
}
