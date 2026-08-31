using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_MiniProject.ViewModels.Authors;

namespace MVC_MiniProject.ViewModels.News
{
    public class NewsCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [ValidateNever]
        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}
