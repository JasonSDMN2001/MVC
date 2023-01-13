using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace WebApplication1.Models;

[Table("course")]
public partial class Course
{
    public Course()
    {
       CourseHasStudents  = new HashSet<CourseHasStudent>();
    }

    [Key]
    [Column("idCOURSE")]
    public int IdCourse { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CourseTitle { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CourseSemaster { get; set; }

    [Column("AFM")]
    public int Afm { get; set; }

    [ForeignKey("Afm")]
    [InverseProperty("Courses")]
    public virtual Professor AfmNavigation { get; set; } = null!;

    [InverseProperty("IdCourseNavigation")]
    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();
}
