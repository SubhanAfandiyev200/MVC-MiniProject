using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.CourseInfos;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseInfoController : Controller
    {
        private readonly ICourseInfoService _courseInfoService;
        private readonly ITeacherService _teacherService;

        public CourseInfoController(ICourseInfoService courseInfoService, ITeacherService teacherService)
        {
            _courseInfoService = courseInfoService;
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _courseInfoService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var courseDetail = await _courseInfoService.GetDetailAsync(id);
                return View(courseDetail);
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
            return View(new CourseInfoCreateVM
            {
                Teachers = await _teacherService.GetAllAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseInfoCreateVM request)
        {
            request.Teachers = await _teacherService.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (!request.Teachers.Any(m => m.Id.ToString() == request.TeacherId))
            {
                ModelState.AddModelError(nameof(request.TeacherId), "The teacher with this id does not exist");
                return View(request);
            }

            if (request.Images == null || !request.Images.Any())
            {
                ModelState.AddModelError(nameof(request.Images), "At least one image is required");
                return View(request);
            }

            await _courseInfoService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var editVM = await _courseInfoService.GetEditVMAsync(id);
                editVM.Teachers = await _teacherService.GetAllAsync();
                return View(editVM);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseInfoEditVM request)
        {
            if (!ModelState.IsValid)
            {
                await _courseInfoService.PopulateEditVMAsync(request, id);
                request.Teachers = await _teacherService.GetAllAsync();
                return View(request);
            }

            try
            {
                await _courseInfoService.EditAsync(id, request);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("TeacherId", "The teacher with this id does not exist");
                request.Teachers = await _teacherService.GetAllAsync();
                return View(request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _courseInfoService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMainImage(int courseInfoId, int imageId)
        {
            try
            {
                await _courseInfoService.UpdateMainImageAsync(courseInfoId, imageId);
                return Ok(new { success = true });
            }
            catch (NotFoundException)
            {
                return NotFound(new { success = false, message = "Course or image not found" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                await _courseInfoService.DeleteImageAsync(imageId);
                return Ok(new { success = true });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (NotFoundException)
            {
                return NotFound(new { success = false, message = "Image not found" });
            }
        }
    }
}
