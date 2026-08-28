using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIconService _iconService;
        private readonly ISliderService _sliderService;
        private readonly ISettingService _settingService;
        private readonly IEventService _eventService;
        private readonly INewsService _newsService;
        private readonly ICourseInfoService _courseInfoService;

        public HomeController(IIconService iconService,
                              ISliderService sliderService,
                              ISettingService settingService,
                              IEventService eventService,
                              INewsService newsService,
                              ICourseInfoService courseInfoService)
        {
            _iconService = iconService;
            _sliderService = sliderService;
            _settingService = settingService;
            _eventService = eventService;
            _newsService = newsService;
            _courseInfoService = courseInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var icons = await _iconService.GetAllUIAsync();
            var sliders = await _sliderService.GetAllUIAsync();
            var settings = await _settingService.GetAllUIAsync();
            var events = await _eventService.GetAllUIAsync();
            var news = await _newsService.GetAllUIAsync();
            var courseInfos = await _courseInfoService.GetAllUIAsync();
            return View(new HomeVM
            {
                Icons = icons,
                Sliders = sliders,
                Settings = settings,
                Events = events,
                News = news,
                CourseInfos = courseInfos
            });
        }
    }
}
