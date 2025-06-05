using System.Reflection;
using CourseService.Infrastructure.Context;
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

            services.AddAutoMapper(typeof(SubjectProfile));

            return services;
        }
    }
}
