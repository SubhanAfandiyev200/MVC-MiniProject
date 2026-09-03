using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accService;
        public AccountController(IAccountService accService)
        {
            _accService = accService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _accService.GetAllUsersAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            await _accService.CreateRolesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
