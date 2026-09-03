using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(ISettingService settingService,
                                   UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string fullname = "";
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                fullname = user.FullName;
            }
            var settings = await _settingService.GetAllUIAsync();
            return View(new HeaderVM
            {
                Settings = settings,
                UserFullName = fullname,
            });
        }
    }
}
