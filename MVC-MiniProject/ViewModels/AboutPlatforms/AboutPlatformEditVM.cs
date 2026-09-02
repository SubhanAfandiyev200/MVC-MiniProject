using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.AboutPlatforms
{
    public class AboutPlatformEditVM
    {
        public IFormFile? Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ExistingImage { get; set; }
    }
}
