using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels;

namespace MVC_MiniProject.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
