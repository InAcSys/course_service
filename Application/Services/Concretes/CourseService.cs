using CourseService.Application.Services.Abstracts;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace CourseService.Application.Services.Concretes
{
    public class CourseService : Service<Course, Guid>
    {
        public CourseService(IValidator<Course> validator, IRepository<Course, Guid> repository) : base(validator, repository)
        {
        }
    }
}