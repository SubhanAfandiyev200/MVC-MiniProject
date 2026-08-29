using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Controllers
{
    public class DetailController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
