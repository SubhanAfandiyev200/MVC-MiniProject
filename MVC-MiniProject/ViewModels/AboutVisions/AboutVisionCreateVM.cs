using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.AboutVisions
{
    public class AboutVisionCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
