using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Settings;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _settingService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var setting = await _settingService.GetByIdAsync(id);
                return View(new SettingEditVM
                {
                    Key = setting.Key,
                    Value = setting.Value,
                    ExistingValue = setting.Value
                });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingEditVM request)
        {
            bool isLogo = string.Equals(request.Key, "logo", StringComparison.OrdinalIgnoreCase);

            if (isLogo)
            {
                ModelState.Remove(nameof(request.Value));
            }
            else
            {
                ModelState.Remove(nameof(request.Image));
                if (string.IsNullOrWhiteSpace(request.Value))
                {
                    ModelState.AddModelError(nameof(request.Value), "Value is required");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _settingService.EditAsync(id, request);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
    }
}
