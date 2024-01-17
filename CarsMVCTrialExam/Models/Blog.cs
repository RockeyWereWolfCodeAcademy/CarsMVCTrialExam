using System.ComponentModel.DataAnnotations;

namespace CarsMVCTrialExam.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required, MaxLength(16)]
        public string Title { get; set; }
        [Required, MaxLength(64)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
