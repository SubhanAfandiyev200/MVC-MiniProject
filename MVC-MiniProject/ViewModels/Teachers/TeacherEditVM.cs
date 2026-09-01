using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_MiniProject.ViewModels.Positions;
using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Teachers
{
    public class TeacherEditVM
    {
        public IFormFile? Image { get; set; }
        public string? ExistingImage { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public int PositionId { get; set; }
        [ValidateNever]
        public IEnumerable<PositionVM> Positions { get; set; }
    }
}
