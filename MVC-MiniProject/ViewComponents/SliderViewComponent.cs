using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderService.GetAllUIAsync();
            return View(sliders);
        }
    }
}
