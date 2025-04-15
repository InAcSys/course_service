using CourseService.Application.Services.Abstracts;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;
using FluentValidation;

namespace CourseService.Application.Services.Concretes
{
    public class SubjectService : Service<Subject, int>
    {
        public SubjectService(IValidator<Subject> validator, IRepository<Subject, int> repository) : base(validator, repository)
        {
        }
    }
}