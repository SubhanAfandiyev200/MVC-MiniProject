using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IAuthorService _authorService;
        public NewsController(INewsService newsService, IAuthorService authorService)
        {
            _newsService = newsService;
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _newsService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var newsDetail = await _newsService.GetDetailAsync(id);
                return View(newsDetail);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            return View(new NewsCreateVM
            {
                Authors = await _authorService.GetAllAsync()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(NewsCreateVM request)
        {
            request.Authors = await _authorService.GetAllAsync();
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            if (!request.Authors.Any(m => m.Id.ToString() == request.AuthorId))
            {
                ModelState.AddModelError(nameof(request.AuthorId), "The author with id does not exist");
                return View(request);
            }
            await _newsService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _newsService.DeleteAsync(id);
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
                var news = await _newsService.GetByIdAsync(id);
                var authors = await _authorService.GetAllAsync(); // ƏLAVƏ ET

                return View(new NewsEditVM
                {
                    Question = news.Question,
                    Date = news.Date,
                    ExistingImage = news.Image,
                    AuthorId = news.AuthorId,
                    Authors = authors // ƏLAVƏ ET
                });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _newsService.EditAsync(id, request);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("AuthorId", "The author with id does not exist");
                request.Authors = await _authorService.GetAllAsync();
                return View(request);
            }
        }
      
    }
}
