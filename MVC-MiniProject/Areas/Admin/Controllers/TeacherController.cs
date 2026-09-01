using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.News;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IPositionService _positionService;
        public TeacherController(ITeacherService teacherService,
                                 IPositionService positionService)
        {
            _teacherService = teacherService;
            _positionService = positionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _teacherService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _teacherService.GetDetailAsync(id);
                return View(result);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new TeacherCreateVM
            {
                Positions = await _positionService.GetAllAsync()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCreateVM request)
        {
            request.Positions = await _positionService.GetAllAsync();
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            if (!request.Positions.Any(m => m.Id.ToString() == request.PositionId))
            {
                ModelState.AddModelError(nameof(request.PositionId), "The position with id does not exist");
                return View(request);
            }
            await _teacherService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _teacherService.DeleteAsync(id);
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
                var teacher = await _teacherService.GetByIdAsync(id);
                var position = await _positionService.GetAllAsync(); // ƏLAVƏ ET

                return View(new TeacherEditVM
                {
                    Fullname = teacher.FullName,
                    ExistingImage = teacher.Image,
                    PositionId = teacher.PositionId,
                    Positions = position // ƏLAVƏ ET
                });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _teacherService.EditAsync(id, request);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("PositionId", "The position with id does not exist");
                request.Positions = await _positionService.GetAllAsync();
                return View(request);
            }
        }
    }
}
