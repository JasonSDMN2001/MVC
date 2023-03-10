using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class MVCDBContext : DbContext
{
    public MVCDBContext()
    {
    }

    public MVCDBContext(DbContextOptions<MVCDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseHasStudent> CourseHasStudents { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Secretary> Secretaries { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-T1N3S96\\SQLEXPRESS01;Database=MVCDb;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.IdCourse).ValueGeneratedNever();

            entity.HasOne(d => d.AfmNavigation).WithMany(p => p.Courses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_professors");
        });

        modelBuilder.Entity<CourseHasStudent>(entity =>
        {
            entity.Property(e => e.Pk).ValueGeneratedNever();

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.CourseHasStudents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_has_students_course");

            entity.HasOne(d => d.RegistrationNumberNavigation).WithMany(p => p.CourseHasStudents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_has_students_students");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.Property(e => e.Afm).ValueGeneratedNever();

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Professors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_professors_users");
        });

        modelBuilder.Entity<Secretary>(entity =>
        {
            entity.Property(e => e.Phonenumber).ValueGeneratedNever();

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Secretaries).HasConstraintName("FK_secretaries_users");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.RegistrationNumber).ValueGeneratedNever();

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Students).HasConstraintName("FK_students_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
