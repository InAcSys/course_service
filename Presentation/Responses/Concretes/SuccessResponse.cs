using CourseService.Presentation.Responses.Abstracts;

namespace CourseService.Presentation.Responses.Concretes
{
    public class SuccessResponse<T>(int statusCode, string message, T data)
        : Response(statusCode, message)
    {
        public T? Data { get; set; } = data;
    }
}
