using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Metadata
{
    public class ProfessorMetadata
    {
        [Display(Name ="Username")]
        public virtual User UsernameNavigation { get; set; } = null!;

        [Display(Name ="Name")]
        public string? Name { get; set; }
        [Display(Name ="Surname")]
        public string? Surname { get; set; }


    }
}
