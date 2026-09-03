using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Icons;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IconController : Controller
    {
        private readonly IIconService _iconService;
        public IconController(IIconService iconService)
        {
            _iconService = iconService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _iconService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _iconService.GetDetailAsync(id);
                return View(result);
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
        public async Task<IActionResult> Create(IconCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            } 
            await _iconService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _iconService.DeleteAsync(id);
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
                var icon = await _iconService.GetByIdAsync(id);
                return View(new IconEditVM
                {
                    ExistingImage = icon.Name
                });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IconEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _iconService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
