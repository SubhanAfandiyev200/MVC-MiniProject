namespace MVC_MiniProject.ViewModels.CourseInfos
{
    public class CourseInfoVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public string Teacher { get; set; }
        public int ImageCount { get; set; }
        public string MainImage { get; set; }
    }
}
