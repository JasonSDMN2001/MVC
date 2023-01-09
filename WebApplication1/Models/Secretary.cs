using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("secretaries")]
public partial class Secretary
{
    [Key]
    public long Phonenumber { get; set; }

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

    [ForeignKey("Username")]
    [InverseProperty("Secretaries")]
    public virtual User? UsernameNavigation { get; set; }
}
