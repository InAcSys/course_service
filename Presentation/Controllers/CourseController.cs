using AutoMapper;
using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.Courses;
using CourseService.Domain.DTOs.CourseSubjects;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CourseController(ICourseService service, IMapper mapper) : ControllerBase
    {
        protected readonly ICourseService _service = service;
        private readonly IMapper _mapper = mapper;

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

            var response = new SuccessResponse<PaginatedResponseDTO<Course>>(
                200,
                "Courses retrieved successfully.",
                new PaginatedResponseDTO<Course>(result.ToList(), size, pageNumber, pageSize)
            );

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "Course not found", null));

            return Ok(new SuccessResponse<Course>(200, "Course found", result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromQuery] Guid tenantId,
            [FromBody] CreateCourseDTO course
        )
        {
            if (course is null)
                return BadRequest();

            var currentCourse = _mapper.Map<Course>(course);
            currentCourse.TenantId = tenantId;

            var createdCourse = await _service.Create(currentCourse);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCourse.Id, tenantId = tenantId },
                new SuccessResponse<Course>(201, "Course created", createdCourse)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateCourseDTO course,
            [FromQuery] Guid tenantId
        )
        {
            if (course is null)
                return BadRequest();

            var currentCourse = _mapper.Map<Course>(course);
            var updatedCourse = await _service.Update(id, currentCourse, tenantId);

            if (updatedCourse is null)
                return NotFound(new ErrorResponse(404, "Course not found", null));

            return Ok(
                new SuccessResponse<Course>(200, "Course updated successfully", updatedCourse)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return NotFound(new ErrorResponse(404, "Course not found", null));

            return Ok(new SuccessResponse<bool>(200, "Course deleted successfully", result));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid tenantId = default,
            [FromQuery] string search = ""
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

            var result = await _service.Search(pageNumber, pageSize, tenantId, search);
            var size = await _service.CountSearchResults(search, tenantId);

            var response = new SuccessResponse<PaginatedResponseDTO<Course>>(
                200,
                "Search completed successfully.",
                new PaginatedResponseDTO<Course>(result.ToList(), size, pageNumber, pageSize)
            );

            return Ok(response);
        }

        [HttpPost("assign-subjects")]
        public async Task<IActionResult> AssignSubjects(
            [FromBody] CourseSubjectsDTO subjects,
            [FromQuery] Guid tenantId
        )
        {
            if (!subjects.SubjectsIds.Any())
            {
                var error = new ErrorResponse(400, "Empty subjects", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.AssignSubjects(subjects, tenantId);
            if (!result)
            {
                var error = new ErrorResponse(
                    400,
                    "Could not be completed with the assignments",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }
            var response = new SuccessResponse<bool>(
                201,
                "Assignment successfully completed",
                result
            );
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("revoke-subjects")]
        public async Task<IActionResult> RevokeSubjects(
            [FromQuery] Guid tenantId,
            [FromBody] CourseSubjectsDTO subjects
        )
        {
            if (!subjects.SubjectsIds.Any())
            {
                var error = new ErrorResponse(400, "Empty subjects", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.RevokeSubjects(subjects, tenantId);
            if (!result)
            {
                var error = new ErrorResponse(
                    400,
                    "Could not be completed with the revokings",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }
            var response = new SuccessResponse<bool>(201, "Revoke successfully completed", result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
