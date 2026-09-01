using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Logo { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
