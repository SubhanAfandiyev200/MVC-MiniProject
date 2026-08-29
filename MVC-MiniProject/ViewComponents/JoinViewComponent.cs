using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class JoinViewComponent : ViewComponent
    {
        private readonly ISettingService _settignService;
        public JoinViewComponent(ISettingService settingService)
        {
            _settignService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settignService.GetAllUIAsync();
            return View(settings);
        }
    }
}
