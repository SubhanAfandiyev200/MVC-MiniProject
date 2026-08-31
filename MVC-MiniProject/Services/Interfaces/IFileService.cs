namespace MVC_MiniProject.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task DeleteFileAsync(string file, string folder);
    }
}
