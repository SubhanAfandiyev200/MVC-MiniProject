using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
