using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Controllers
{
    public class DetailController : Controller
    {
        private readonly ICourseInfoService _courseService;
        public DetailController(ICourseInfoService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var courseDetail = await _courseService.GetDetailUIAsync(id);
            return View(courseDetail);
        }
    }
}
