using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MVC_MiniProject.Helpers.Enums;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Account;

namespace MVC_MiniProject.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<AppUser> signInManager,
                              IHttpContextAccessor httpContextAccessor,
                              LinkGenerator linkGenerator,
                              IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _emailService = emailService;
        }

        public async Task ConfirmUserEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task CreateRolesAsync()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                }
            }
        }

        public async Task<IEnumerable<AccountVM>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(m => new AccountVM
            {
                Email = m.Email,
                FullName = m.FullName,
                Id = m.Id,
                UserName = m.UserName
            });
        }

        public async Task<LoginResponse> LoginAsync(LoginVM model)
        {
            // Find user by email or username
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            }
            
            if (user is null)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    AppUser = null
                };
            }

            // Check if email is confirmed
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    AppUser = null
                };
            }

            // Check password - returns SignInResult, not null
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            
            if (!result.Succeeded)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    AppUser = null
                };
            }

            return new LoginResponse
            {
                IsSuccess = true,
                AppUser = user
            };
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM model)
        {
            AppUser user = new()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return result;
            }
            
            // Assign default Member role to new users
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Generate the confirmation link with proper scheme and host
            var request = _httpContextAccessor.HttpContext.Request;
            string link = $"{request.Scheme}://{request.Host}/Account/ConfirmAccount?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // Create a nice HTML email template
            string emailBody = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
                        .container {{ max-width: 600px; margin: 50px auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
                        .header {{ background-color: #1a73e8; color: #ffffff; padding: 30px; text-align: center; }}
                        .content {{ padding: 40px 30px; }}
                        .button {{ display: inline-block; padding: 15px 40px; background-color: #1a73e8; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold; margin: 20px 0; }}
                        .footer {{ background-color: #f8f9fa; padding: 20px; text-align: center; color: #666; font-size: 12px; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Welcome to Fiorello!</h1>
                        </div>
                        <div class='content'>
                            <h2>Hi {user.FullName},</h2>
                            <p>Thank you for registering! Please confirm your email address to activate your account.</p>
                            <p style='text-align: center;'>
                                <a href='{link}' class='button'>Confirm Email Address</a>
                            </p>
                            <p style='color: #666; font-size: 14px;'>If the button doesn't work, copy and paste this link into your browser:</p>
                            <p style='color: #1a73e8; word-break: break-all; font-size: 12px;'>{link}</p>
                        </div>
                        <div class='footer'>
                            <p>If you didn't create this account, please ignore this email.</p>
                            <p>&copy; 2024 Fiorello. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            await _emailService.SendEmailAsync(
                user.Email,
                "Fiorello - Email Confirmation",
                emailBody);

            return result;
        }
    }
}
