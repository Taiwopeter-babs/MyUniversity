using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Models;

namespace MyUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Course>().ToTable("Course");
           modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
           modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
