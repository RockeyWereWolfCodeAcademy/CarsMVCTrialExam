using System.ComponentModel.DataAnnotations;

namespace CarsMVCTrialExam.Areas.Admin.ViewModels.AdminBlogVM
{
    public class AdminBlogUpdateVM
    {
        [MaxLength(16)]
        public string Title { get; set; }
        [MaxLength(64)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
