using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_MiniProject.ViewModels.Authors;
using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.News
{
    public class NewsEditVM
    {
        public IFormFile? Image { get; set; }

        public string? ExistingImage { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [ValidateNever]
        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}