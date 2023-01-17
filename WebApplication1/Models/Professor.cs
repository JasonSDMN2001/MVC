using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("professors")]
public partial class Professor
{
    public Professor()
    {
        Courses = new HashSet<Course>();
    }
   
    [Key]
    [Column("Afm")]
    public int Afm { get; set; }

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
    public string Username { get; set; } = null!;

    [InverseProperty("AfmNavigation")]
    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    [ForeignKey("Username")]
    [InverseProperty("Professors")]
    public virtual User UsernameNavigation { get; set; } = null!;
}
