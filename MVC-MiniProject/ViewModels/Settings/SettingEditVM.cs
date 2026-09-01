namespace MVC_MiniProject.ViewModels.Settings
{
    public class SettingEditVM
    {
        public string Key { get; set; }
        public string? Value { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingValue { get; set; }
    }
}
