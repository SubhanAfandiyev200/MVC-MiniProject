using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _authorService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var eventDetail = await _authorService.GetDetailAsync(id);
                return View(eventDetail);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
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
                var authors = await _authorService.GetByIdAsync(id);
                return View(new AuthorEditVM
                {
                    Fullname = authors.FullName
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
        public async Task<IActionResult> Edit(int id, AuthorEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _authorService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
