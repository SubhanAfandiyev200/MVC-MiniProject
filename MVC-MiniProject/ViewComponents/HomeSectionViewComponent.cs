using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class HomeSectionViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public HomeSectionViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingService.GetAllUIAsync();
            return View(settings);
        }
    }
}
