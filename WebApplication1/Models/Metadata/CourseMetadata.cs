using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Metadata
{
    public class CourseMetadata
    {
        [Display(Name = "Course")]
        public string? CourseTitle { get; set; }
        [Display(Name ="Semaster")]
        public string? CourseSemaster { get; set; }
    }
}
