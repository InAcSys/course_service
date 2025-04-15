using System.Reflection;
using CourseService.Application.Services.Concretes;
using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Context;
using CourseService.Infrastructure.Repositories.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Presentation.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var connection = Environment.GetEnvironmentVariable("COURSE_SERVICE_DATABASE_STRING_CONNECTION");
            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentException("Connection string is not set.");
            }
            services.AddDbContext<DbContext, CourseServiceDbContext>(
                options => options.UseNpgsql(
                    connection,
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                )
            );

            services.AddScoped<IService<Course, Guid>, CourseService.Application.Services.Concretes.CourseService>();
            services.AddScoped<IService<AcademicProgram, int>, AcademicProgramService>();
            services.AddScoped<IService<AcademicLevel, int>, AcademicLevelService>();
            services.AddScoped<IService<Subject, int>, SubjectService>();
            services.AddScoped<IRepository<Course, Guid>, CourseRepository>();
            services.AddScoped<IRepository<AcademicProgram, int>, AcademicProgramRepository>();
            services.AddScoped<IRepository<AcademicLevel, int>, AcademicLevelRepository>();
            services.AddScoped<IRepository<Subject, int>, SubjectRepository>();

            services.AddValidatorsFromAssemblyContaining<CourseValidator>();
            services.AddValidatorsFromAssemblyContaining<AcademicProgramValidator>();
            services.AddValidatorsFromAssemblyContaining<AcademicLevelValidator>();
            services.AddValidatorsFromAssemblyContaining<SubjectValidator>();

            return services;
        }
    }
}