using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Authors;
using MVC_MiniProject.ViewModels.Positions;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            bool isExist = await _positionService.ExistAsync(request.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Position already exists!");
                return View(request);
            }
            await _positionService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _positionService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var positionDetail = await _positionService.GetDetailAsync(id);
                return View(positionDetail);
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
                var position = await _positionService.GetByIdAsync(id);
                return View(new PositionEditVM
                {
                    Name = position.Name
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
        public async Task<IActionResult> Edit(int id, PositionEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            bool isExist = await _positionService.ExistAsync(request.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Position already exists!");
                return View(request);
            }
            await _positionService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
