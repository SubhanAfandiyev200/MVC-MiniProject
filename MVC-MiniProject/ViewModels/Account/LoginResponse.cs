using MVC_MiniProject.Models;

namespace MVC_MiniProject.ViewModels.Account
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public AppUser AppUser { get; set; }
    }
}
