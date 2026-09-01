using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Icons
{
    public class IconEditVM
    {
        [Required]
        public IFormFile? Name { get; set; }
        public string? ExistingImage { get; set; }
    }
}
