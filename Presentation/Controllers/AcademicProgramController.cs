using AutoMapper;
using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.AcademicPrograms;
using CourseService.Domain.DTOs.ProgramLevels;
using CourseService.Domain.DTOs.ProgramSubjects;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AcademicProgramController(IProgramService service, IMapper mapper) : ControllerBase
    {
        protected readonly IProgramService _service = service;
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

        [HttpPost("assign-subjects")]
        public async Task<IActionResult> AssignSubjects(
            [FromBody] ProgramSubjectsDTO subjects,
            [FromQuery] Guid tenantId
        )
        {
            if (!subjects.Subjects.Any())
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
            [FromBody] ProgramSubjectsDTO subjects
        )
        {
            if (!subjects.Subjects.Any())
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

        [HttpPost("assign-levels")]
        public async Task<IActionResult> AssignLevels(
            [FromBody] ProgramLevelsDTO levels,
            [FromQuery] Guid tenantId
        )
        {
            if (!levels.Levels.Any())
            {
                var error = new ErrorResponse(400, "Empty levels", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.AssignLevels(levels, tenantId);
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

        [HttpDelete("revoke-levels")]
        public async Task<IActionResult> RevokeLevels(
            [FromQuery] Guid tenantId,
            [FromBody] ProgramLevelsDTO levels
        )
        {
            if (!levels.Levels.Any())
            {
                var error = new ErrorResponse(400, "Empty levels", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.RevokeLevels(levels, tenantId);
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
