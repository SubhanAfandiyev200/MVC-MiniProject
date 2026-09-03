
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Account;

namespace FiorelloMWS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IAccountService accService,
                                 SignInManager<AppUser> signInManager)
        {
            _accService = accService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _accService.RegisterAsync(request);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(request);
            }

            // create email message


            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accService.LoginAsync(request);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Login failed!");
                return View();
            }
            await _signInManager.SignInAsync(result.AppUser, false, null);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmAccount(string userId, string token)
        {
            await _accService.ConfirmUserEmailAsync(userId, token);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            await _accService.CreateRolesAsync();
            return Content("Roles created successfully! You can now register users.");
        }
    }
}
