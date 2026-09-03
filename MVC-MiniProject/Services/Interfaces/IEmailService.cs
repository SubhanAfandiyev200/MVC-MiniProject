namespace MVC_MiniProject.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string htmlBody);
    }
}
