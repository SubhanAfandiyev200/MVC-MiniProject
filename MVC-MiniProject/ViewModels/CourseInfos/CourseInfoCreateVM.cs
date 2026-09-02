using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_MiniProject.ViewModels.Teachers;

namespace MVC_MiniProject.ViewModels.CourseInfos
{
    public class CourseInfoCreateVM
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Sales count is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Sales count must be a positive number")]
        public int SalesCount { get; set; }

        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }

        [Required(ErrorMessage = "Teacher is required")]
        public string TeacherId { get; set; }

        [Required(ErrorMessage = "At least one image is required")]
        public List<IFormFile> Images { get; set; }

        [ValidateNever]
        public IEnumerable<TeacherVM> Teachers { get; set; }
    }
}
