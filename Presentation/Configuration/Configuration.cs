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

            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<
                ICourseService,
                CourseService.Application.Services.Concretes.CourseService
            >();
            services.AddScoped<ILevelService, AcademicLevelService>();
            services.AddScoped<IProgramService, AcademicProgramService>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IRepository<SubjectRequisite, int>, SubjectRequisitesRepository>();
            services.AddScoped<IRepository<SubjectProgram, int>, SubjectProgramRepository>();
            services.AddScoped<IRepository<CourseSubject, int>, CourseSubjectRepository>();
            services.AddScoped<IRepository<ProgramLevel, int>, ProgramLevelRepository>();
            services.AddScoped<IRepository<ProgramSubject, int>, ProgramSubjectRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILevelRepository, AcademicLevelRepository>();
            services.AddScoped<IProgramRepository, AcademicProgramRepository>();

            services.AddAutoMapper(typeof(SubjectProfile));
            services.AddAutoMapper(typeof(CourseProfile));
            services.AddAutoMapper(typeof(AcademicLevelProfile));
            services.AddAutoMapper(typeof(AcademicProgramProfile));

            return services;
        }
    }
}
