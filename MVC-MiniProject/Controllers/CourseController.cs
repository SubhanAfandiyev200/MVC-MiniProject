using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniProject.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
