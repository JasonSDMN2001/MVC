using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("course_has_students")]
public partial class CourseHasStudent
{
    [Column("idCOURSE")]
    public int IdCourse { get; set; }

    public int RegistrationNumber { get; set; }

    public int? GradeCourseStudent { get; set; }

    [Key]
    [Column("pk")]
    public int Pk { get; set; }

    [ForeignKey("IdCourse")]
    [InverseProperty("CourseHasStudents")]
    public virtual Course IdCourseNavigation { get; set; } = null!;

    [ForeignKey("RegistrationNumber")]
    [InverseProperty("CourseHasStudents")]
    public virtual Student RegistrationNumberNavigation { get; set; } = null!;
}
