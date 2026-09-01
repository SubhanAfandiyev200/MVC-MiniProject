using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Sliders
{
    public class SliderEditVM
    {
        [Required]
        public IFormFile Logo { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ExistingImage { get; set; }
        public string? ExistingLogo { get; set; }
    }
}
