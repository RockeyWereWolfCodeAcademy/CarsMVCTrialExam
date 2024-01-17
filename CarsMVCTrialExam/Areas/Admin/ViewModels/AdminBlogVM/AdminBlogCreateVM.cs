using System.ComponentModel.DataAnnotations;

namespace CarsMVCTrialExam.Areas.Admin.ViewModels.AdminBlogVM
{
    public class AdminBlogCreateVM
    {
        [Required, MaxLength(16)]
        public string Title { get; set; }
        [Required, MaxLength(64)]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
