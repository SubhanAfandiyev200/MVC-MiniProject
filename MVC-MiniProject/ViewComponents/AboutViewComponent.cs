using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.MainAbouts;

namespace MVC_MiniProject.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutPlatformService _aboutPlatformService;
        private readonly IAboutVisionService _aboutVisionService;
        public AboutViewComponent(IAboutPlatformService aboutPlatformService,
                               IAboutVisionService aboutVisionService)
        {
            _aboutPlatformService = aboutPlatformService;
            _aboutVisionService = aboutVisionService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutPlatform = await _aboutPlatformService.GetUIAsync();
            var aboutVision = await _aboutVisionService.GetUIAsync();
            return View(new MainAboutVM
            {
                AboutPlatforms = aboutPlatform,
                AboutVisions = aboutVision
            });
        }
    }
}
