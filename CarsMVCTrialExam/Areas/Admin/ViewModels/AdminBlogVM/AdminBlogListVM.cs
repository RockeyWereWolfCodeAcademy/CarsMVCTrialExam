using System.ComponentModel.DataAnnotations;

namespace CarsMVCTrialExam.Areas.Admin.ViewModels.AdminBlogVM
{
    public class AdminBlogListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
    }
}
