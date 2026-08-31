using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task DeleteFileAsync(string file, string folder)
        {
            string path = Path.Combine(_env.WebRootPath, folder, file);
            if (File.Exists(path))
                File.Delete(path);
            await Task.CompletedTask;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path = Path.Combine(_env.WebRootPath, folder, fileName);

            using FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }
}
