using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class SearchCourseViewComponent : ViewComponent
    {
        private readonly ICourseInfoService _courseInfoService;
        public SearchCourseViewComponent(ICourseInfoService courseInfoService)
        {
            _courseInfoService = courseInfoService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchText)
        {
            var courses = await _courseInfoService.GetSearchedCourseUIAsync(searchText);
            return View(courses);
        }
    }
}
