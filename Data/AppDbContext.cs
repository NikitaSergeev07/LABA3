using System.Data.Common;
using LABA3.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace LABA3.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<StudentUser> StudentUsers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<College> Colleges { get; set; }
    public DbSet<Course> Courses { get; set; }
}