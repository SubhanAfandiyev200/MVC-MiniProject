using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.ViewComponents
{
    public class CoursesViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ICourseInfoService _courseInfoService;
        public CoursesViewComponent(ISettingService settingService,
                                    ICourseInfoService courseInfoService)
        {
            _settingService = settingService;
            _courseInfoService = courseInfoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingService.GetAllUIAsync();
            var courseInfos = await _courseInfoService.GetAllUIAsync();
            return View(new CourseVM
            {
                Settings = settings,
                CourseInfos = courseInfos
            });
        }
    }
}
