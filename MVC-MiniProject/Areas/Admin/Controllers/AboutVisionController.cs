using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.AboutPlatforms;
using MVC_MiniProject.ViewModels.AboutVisions;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AboutVisionController : Controller
    {
        private readonly IAboutVisionService _aboutVisionService;

        public AboutVisionController(IAboutVisionService aboutVisionService)
        {
            _aboutVisionService = aboutVisionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutVisionService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                return View(await _aboutVisionService.GetDetailAsync(id));
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AboutVisionCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _aboutVisionService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aboutVisionService.DeleteAsync(id);
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
                var vision = await _aboutVisionService.GetByIdAsync(id);
                return View(new AboutVisionEditVM
                {
                    ExistingImage = vision.Image,
                    Description = vision.Description,
                    Title = vision.Title
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
        public async Task<IActionResult> Edit(int id, AboutVisionEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _aboutVisionService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
