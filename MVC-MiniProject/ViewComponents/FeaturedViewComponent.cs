using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class FeaturedViewComponent : ViewComponent
    {
        private readonly ICourseInfoService _courseInfoService;
        public FeaturedViewComponent(ICourseInfoService courseInfoService)
        {
            _courseInfoService = courseInfoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _courseInfoService.GetAllUIAsync();
            return View(courses);
        }
    }
}
