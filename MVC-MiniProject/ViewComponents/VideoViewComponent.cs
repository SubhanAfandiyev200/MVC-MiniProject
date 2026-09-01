using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.ViewComponents
{
    public class VideoViewComponent : ViewComponent
    {
        private readonly IVideoService _videoService;
        public VideoViewComponent(IVideoService videoService)
        {
            _videoService = videoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var videos = await _videoService.GetUIAsync();
            return View(videos);
        }
    }
}
