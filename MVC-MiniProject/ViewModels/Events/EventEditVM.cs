using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Events
{
    public class EventEditVM
    {
        [Required(ErrorMessage = "Day is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers/digits can be entered")]
        [Range(1, 31, ErrorMessage = "No such month with these date day")]
        public string DateDay { get; set; }
        [Required]
        public string DateMonth { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
