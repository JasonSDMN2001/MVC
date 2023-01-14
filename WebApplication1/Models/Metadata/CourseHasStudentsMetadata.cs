using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Metadata
{
    public class CourseHasStudentsMetadata
    {
        [Display(Name ="AM")]
        public virtual Student RegistrationNumberNavigation { get; set; } = null!;
        [Display(Name ="Course")]
        public virtual Course IdCourseNavigation { get; set; } = null!;
        [Display(Name ="Grade")]
        public int? GradeCourseStudent { get; set; }

    }
}
