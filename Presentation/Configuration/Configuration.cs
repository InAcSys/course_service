using System.Reflection;
using CourseService.Application.Services.Concretes;
using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Concretes.AcademicLevels;
using CourseService.Application.Validators.Concretes.AcademicPrograms;
using CourseService.Application.Validators.Concretes.Courses;
using CourseService.Application.Validators.Concretes.Subjects;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Context;
using CourseService.Infrastructure.Repositories.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using CourseService.Presentation.Profiles;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Presentation.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var connection = Environment.GetEnvironmentVariable(
                "COURSE_SERVICE_DATABASE_STRING_CONNECTION"
            );
            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentException("Connection string is not set.");
            }
            Console.WriteLine(connection);
            services.AddDbContext<DbContext, CourseServiceDbContext>(options =>
                options.UseNpgsql(
                    connection,
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                )
            );

            services.AddScoped<ICreateValidator<Subject>, CreateSubjectValidator>();
            services.AddScoped<IUpdateValidator<Subject>, UpdateSubjectValidator>();
            services.AddScoped<ICreateValidator<Course>, CreateCourseValidator>();
            services.AddScoped<IUpdateValidator<Course>, UpdateCourseValidator>();
            services.AddScoped<ICreateValidator<AcademicLevel>, CreateAcademicLevelValidator>();
            services.AddScoped<IUpdateValidator<AcademicLevel>, UpdateAcademicLevelValidator>();
            services.AddScoped<ICreateValidator<AcademicProgram>, CreateAcademicProgramValidator>();
            services.AddScoped<IUpdateValidator<AcademicProgram>, UpdateAcademicProgramValidator>();

            services.AddScoped<IService<Subject, Guid>, SubjectService>();
            services.AddScoped<
                IService<Course, Guid>,
                CourseService.Application.Services.Concretes.CourseService
            >();
            services.AddScoped<IService<AcademicLevel, int>, AcademicLevelService>();
            services.AddScoped<IService<AcademicProgram, int>, AcademicProgramService>();

            services.AddScoped<IRepository<Subject, Guid>, SubjectRepository>();
            services.AddScoped<IRepository<Course, Guid>, CourseRepository>();
            services.AddScoped<IRepository<AcademicLevel, int>, AcademicLevelRepository>();
            services.AddScoped<IRepository<AcademicProgram, int>, AcademicProgramRepository>();

            services.AddAutoMapper(typeof(SubjectProfile));
            services.AddAutoMapper(typeof(CourseProfile));
            services.AddAutoMapper(typeof(AcademicLevelProfile));
            services.AddAutoMapper(typeof(AcademicProgramProfile));

            return services;
        }
    }
}
