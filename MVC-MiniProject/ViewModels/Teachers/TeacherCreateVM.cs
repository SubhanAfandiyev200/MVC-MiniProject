using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_MiniProject.Models;
using MVC_MiniProject.ViewModels.Positions;
using System.ComponentModel.DataAnnotations;

namespace MVC_MiniProject.ViewModels.Teachers
{
    public class TeacherCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string PositionId { get; set; }
        [ValidateNever]
        public IEnumerable<PositionVM> Positions { get; set; }
    }
}
