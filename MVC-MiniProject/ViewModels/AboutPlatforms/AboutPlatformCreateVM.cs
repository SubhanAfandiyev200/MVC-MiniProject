using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.AboutPlatforms
{
    public class AboutPlatformCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
