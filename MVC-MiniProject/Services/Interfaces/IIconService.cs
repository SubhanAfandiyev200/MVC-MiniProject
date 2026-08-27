using MVC_MiniProject.ViewModels.Icons;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IIconService
    {
        Task<IEnumerable<IconUIVM>> GetAllUIAsync();
    }
}
