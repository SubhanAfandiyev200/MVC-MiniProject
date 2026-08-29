using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniProject.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
