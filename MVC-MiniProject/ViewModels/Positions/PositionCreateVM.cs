using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Positions
{
    public class PositionCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
