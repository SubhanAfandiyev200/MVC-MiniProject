namespace MVC_MiniProject.ViewModels.CourseInfos
{
    public class CourseDetailUIVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalesCount { get; set; }
        public string TeacherName { get; set; }
        public CourseImageUIVM[] Images { get; set; }
        public string TeacherImage { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
    }
}
