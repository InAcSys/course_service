using FluentValidation;

namespace CourseService.Application.Validators.Interfaces
{
    public interface IUpdateValidator<in T> : IValidator<T> { }
}
