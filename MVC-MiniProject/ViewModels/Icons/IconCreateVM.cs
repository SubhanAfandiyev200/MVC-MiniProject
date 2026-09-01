using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Icons
{
    public class IconCreateVM
    {
        [Required]
        public IFormFile Name { get; set; }
    }
}
