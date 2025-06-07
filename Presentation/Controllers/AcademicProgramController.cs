using AutoMapper;
using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.AcademicPrograms;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AcademicProgramController(
        ISearchableService<AcademicProgram, int> service,
        IMapper mapper
    ) : ControllerBase
    {
        protected readonly ISearchableService<AcademicProgram, int> _service = service;
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

            var response = new SuccessResponse<PaginatedResponseDTO<AcademicProgram>>(
                200,
                "AcademicPrograms retrieved successfully.",
                new PaginatedResponseDTO<AcademicProgram>(
                    result.ToList(),
                    size,
                    pageNumber,
                    pageSize
                )
            );

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "AcademicProgram not found", null));

            return Ok(new SuccessResponse<AcademicProgram>(200, "AcademicProgram found", result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromQuery] Guid tenantId,
            [FromBody] CreateAcademicProgramDTO academicProgram
        )
        {
            if (academicProgram is null)
                return BadRequest();

            var currentAcademicProgram = _mapper.Map<AcademicProgram>(academicProgram);
            currentAcademicProgram.TenantId = tenantId;

            var createdAcademicProgram = await _service.Create(currentAcademicProgram);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAcademicProgram.Id, tenantId = tenantId },
                new SuccessResponse<AcademicProgram>(
                    201,
                    "AcademicProgram created",
                    createdAcademicProgram
                )
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateAcademicProgramDTO academicProgram,
            [FromQuery] Guid tenantId
        )
        {
            if (academicProgram is null)
                return BadRequest();

            var currentAcademicProgram = _mapper.Map<AcademicProgram>(academicProgram);
            var updatedAcademicProgram = await _service.Update(
                id,
                currentAcademicProgram,
                tenantId
            );

            if (updatedAcademicProgram is null)
                return NotFound(new ErrorResponse(404, "AcademicProgram not found", null));

            return Ok(
                new SuccessResponse<AcademicProgram>(
                    200,
                    "AcademicProgram updated successfully",
                    updatedAcademicProgram
                )
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return NotFound(new ErrorResponse(404, "AcademicProgram not found", null));

            return Ok(
                new SuccessResponse<bool>(200, "AcademicProgram deleted successfully", result)
            );
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

            var response = new SuccessResponse<PaginatedResponseDTO<AcademicProgram>>(
                200,
                "Search completed successfully.",
                new PaginatedResponseDTO<AcademicProgram>(
                    result.ToList(),
                    size,
                    pageNumber,
                    pageSize
                )
            );

            return Ok(response);
        }
    }
}
