using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.AboutVisions
{
    public class AboutVisionEditVM
    {
        public IFormFile? Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ExistingImage { get; set; }
    }
}
