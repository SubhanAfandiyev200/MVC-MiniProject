using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class MilestonesViewComponent : ViewComponent
    {
        private readonly IIconService _iconService;
        public MilestonesViewComponent(IIconService iconService)
        {
            _iconService = iconService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var icons = await _iconService.GetAllUIAsync();
            return View(icons);
        }
    }
}
