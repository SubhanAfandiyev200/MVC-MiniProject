namespace MVC_MiniProject.ViewModels.CourseInfos
{
    public class CourseInfoDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public string Teacher { get; set; }
        public int TeacherId { get; set; }
        public List<CourseImageVM> Images { get; set; }
    }
}
