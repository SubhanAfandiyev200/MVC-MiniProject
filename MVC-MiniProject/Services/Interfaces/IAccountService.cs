using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using MVC_MiniProject.ViewModels.Account;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM model);
        Task<LoginResponse> LoginAsync(LoginVM model);
        Task<IEnumerable<AccountVM>> GetAllUsersAsync();
        Task CreateRolesAsync();
        Task ConfirmUserEmailAsync(string userId, string token);
    }
}
