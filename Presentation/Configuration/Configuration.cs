using System.Reflection;
using CourseService.Application.Services.Concretes;
using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Concretes;
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

            services.AddScoped<IService<Subject, Guid>, SubjectService>();

            services.AddScoped<IRepository<Subject, Guid>, SubjectRepository>();

            services.AddAutoMapper(typeof(SubjectProfile));

            return services;
        }
    }
}
