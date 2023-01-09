using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("students")]
public partial class Student
{
    [Key]
    public int RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Surname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Department { get; set; }

    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Username { get; set; }

    [InverseProperty("RegistrationNumberNavigation")]
    public virtual ICollection<CourseHasStudent> CourseHasStudents { get; } = new List<CourseHasStudent>();

    [ForeignKey("Username")]
    [InverseProperty("Students")]
    public virtual User? UsernameNavigation { get; set; }
}
