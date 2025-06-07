using FluentValidation;

namespace CourseService.Application.Validators.Interfaces
{
    public interface ICreateValidator<in T> : IValidator<T> { }
}
