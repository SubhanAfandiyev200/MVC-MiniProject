using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly ITeacherService _teacherService;
        public TeacherViewComponent(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = await _teacherService.GetAllUIAsync();
            return View(teachers);
        }
    }
}
