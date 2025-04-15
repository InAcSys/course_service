using CourseService.Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Infrastructure.Context
{
    public class CourseServiceDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AcademicLevel> AcademicLevels { get; set; }
        public DbSet<AcademicProgram> AcademicPrograms { get; set; }
    }
}