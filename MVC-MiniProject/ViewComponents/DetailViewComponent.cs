using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class DetailViewComponent : ViewComponent
    {
        private readonly ICourseInfoService _courseService;
        public DetailViewComponent(ICourseInfoService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var courseDetail = await _courseService.GetDetailUIAsync(id);
                return View(courseDetail);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        
    }
}
