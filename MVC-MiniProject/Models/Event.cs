namespace MVC_MiniProject.Models
{
    public class Event : BaseEntity
    {
        public string DateDay { get; set; }
        public string DateMonth { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
    }
}
