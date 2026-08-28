using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IIconService _iconService;
        private readonly IAboutPlatformService _aboutPlatformService;
        private readonly IAboutVisionService _aboutVisionService;
        public AboutController(ITeacherService teacherService,
                               IIconService iconService,
                               IAboutPlatformService aboutPlatformService,
                               IAboutVisionService aboutVisionService)
        {
            _teacherService = teacherService;
            _iconService = iconService;
            _aboutPlatformService = aboutPlatformService;
            _aboutVisionService = aboutVisionService;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllUIAsync();
            var icons = await _iconService.GetAllUIAsync();
            var aboutPlatforms = await _aboutPlatformService.GetUIAsync();
            var aboutVisions = await _aboutVisionService.GetUIAsync();
            return View(new AboutVM
            {
                Icons = icons,
                Teachers = teachers,
                AboutPlatforms = aboutPlatforms,
                AboutVisions = aboutVisions
            });
        }
    }
}
