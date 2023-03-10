using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Metadata;

namespace WebApplication1.Models
{
    [ModelMetadataType(typeof(ProfessorMetadata))]
    public partial class Professor
    {
        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {
                return Name + "," + Surname;
            }
        }
    }
    [ModelMetadataType(typeof(CourseMetadata))]
    public partial class Course
    {
       // public virtual ICollection<Professor> AfmProfessors { get; } = new List<Professor>();
    }
    [ModelMetadataType(typeof(CourseHasStudentsMetadata))]
    public partial class CourseHasStudent
    {
        
    }
    [ModelMetadataType(typeof(StudentsMetadata))]
    public partial class Student
    {

    }
}
