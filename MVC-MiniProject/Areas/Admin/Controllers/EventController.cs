using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Exceptions;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Events;

namespace MVC_MiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _eventService.GetAllAsync());
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(EventCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _eventService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet] 
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var eventDetail = await _eventService.GetDetailAsync(id);
                return View(eventDetail);
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _eventService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var events = await _eventService.GetByIdAsync(id);
                return View(new EventEditVM
                {
                    Title = events.Title,
                    DateDay = events.DateDay,
                    DateMonth = events.DateMonth,
                    Location = events.Location
                });
            }
            catch (NotFoundException)
            {
                HttpContext.Response.Redirect("/NotFound/Index");
                return Content(string.Empty);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _eventService.EditAsync(id, request);
            return RedirectToAction(nameof(Index));
        }
    }
}
