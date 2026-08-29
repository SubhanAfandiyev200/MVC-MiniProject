using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniProject.Controllers
{
    public class SearchCourseController : Controller
    {
        public async Task<IActionResult> Index(string searchText)
        {
            return View((object)searchText);
        }
    }
}
