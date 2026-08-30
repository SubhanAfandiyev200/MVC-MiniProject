using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Authors
{
    public class AuthorCreateVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Fullname can only contain letters and spaces")]
        public string Fullname { get; set; }
    }
}