using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.Sliders;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutPlatformController : Controller
    {
        private readonly IAboutPlatformService _aboutPlatformService;

        public AboutPlatformController(IAboutPlatformService aboutPlatformService)
        {
            _aboutPlatformService = aboutPlatformService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutPlatformService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                return View(await _aboutPlatformService.GetDetailAsync(id));
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutPlatformCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _aboutPlatformService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aboutPlatformService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var platform = await _aboutPlatformService.GetByIdAsync(id);
                return View(new AboutPlatformEditVM
                {
                    ExistingImage = platform.Image,
                    Description = platform.Description,
                    Title = platform.Title
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
        public async Task<IActionResult> Edit(int id, AboutPlatformEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _aboutPlatformService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
