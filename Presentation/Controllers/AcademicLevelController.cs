using AutoMapper;
using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.AcademicLevels;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AcademicLevelController(IService<AcademicLevel, int> service, IMapper mapper)
        : ControllerBase
    {
        protected readonly IService<AcademicLevel, int> _service = service;
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

            var response = new SuccessResponse<PaginatedResponseDTO<AcademicLevel>>(
                200,
                "AcademicLevels retrieved successfully.",
                new PaginatedResponseDTO<AcademicLevel>(result.ToList(), size, pageNumber, pageSize)
            );

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "AcademicLevel not found", null));

            return Ok(new SuccessResponse<AcademicLevel>(200, "AcademicLevel found", result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromQuery] Guid tenantId,
            [FromBody] CreateAcademicLevelDTO academicLevel
        )
        {
            if (academicLevel is null)
                return BadRequest();

            var currentAcademicLevel = _mapper.Map<AcademicLevel>(academicLevel);
            currentAcademicLevel.TenantId = tenantId;

            var createdAcademicLevel = await _service.Create(currentAcademicLevel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAcademicLevel.Id, tenantId = tenantId },
                new SuccessResponse<AcademicLevel>(
                    201,
                    "AcademicLevel created",
                    createdAcademicLevel
                )
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateAcademicLevelDTO academicLevel,
            [FromQuery] Guid tenantId
        )
        {
            if (academicLevel is null)
                return BadRequest();

            var currentAcademicLevel = _mapper.Map<AcademicLevel>(academicLevel);
            var updatedAcademicLevel = await _service.Update(id, currentAcademicLevel, tenantId);

            if (updatedAcademicLevel is null)
                return NotFound(new ErrorResponse(404, "AcademicLevel not found", null));

            return Ok(
                new SuccessResponse<AcademicLevel>(
                    200,
                    "AcademicLevel updated successfully",
                    updatedAcademicLevel
                )
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromQuery] Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return NotFound(new ErrorResponse(404, "AcademicLevel not found", null));

            return Ok(new SuccessResponse<bool>(200, "AcademicLevel deleted successfully", result));
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

            var response = new SuccessResponse<PaginatedResponseDTO<AcademicLevel>>(
                200,
                "Search completed successfully.",
                new PaginatedResponseDTO<AcademicLevel>(result.ToList(), size, pageNumber, pageSize)
            );

            return Ok(response);
        }
    }
}
