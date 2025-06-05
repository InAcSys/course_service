using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SubjectController(IService<Subject, Guid> service) : ControllerBase
    {
        private readonly IService<Subject, Guid> _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid tenantId = default
        )
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number and size must be greater than 0.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }

            var result = await _service.GetAll(pageNumber, pageSize, tenantId);
            var size = await _service.Count(tenantId);

            var response = new SuccessResponse<PaginatedResponseDTO<Subject>>(
                200,
                "Users retrieved successfully.",
                new PaginatedResponseDTO<Subject>(result.ToList(), size, pageNumber, pageSize)
            );

            return StatusCode(response.StatusCode, response);
        }
    }
}
