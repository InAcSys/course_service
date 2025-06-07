using AutoMapper;
using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs.Responses;
using CourseService.Domain.DTOs.SubjectRequisites;
using CourseService.Domain.DTOs.Subjects;
using CourseService.Domain.Entities.Concretes;
using CourseService.Presentation.Responses.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SubjectController(ISubjectService service, IMapper mapper) : ControllerBase
    {
        protected readonly ISubjectService _service = service;
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

            var response = new SuccessResponse<PaginatedResponseDTO<Subject>>(
                200,
                "Subjects retrieved successfully.",
                new PaginatedResponseDTO<Subject>(result.ToList(), size, pageNumber, pageSize)
            );

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "Subject not found", null));

            return Ok(new SuccessResponse<Subject>(200, "Subject found", result));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromQuery] Guid tenantId,
            [FromBody] CreateSubjectDTO subject
        )
        {
            try
            {
                if (subject is null)
                    return BadRequest();

                var currentSubject = _mapper.Map<Subject>(subject);
                currentSubject.TenantId = tenantId;

                var createdSubject = await _service.Create(currentSubject);

                var response = new SuccessResponse<Subject>(201, "Subject created", createdSubject);

                return StatusCode(response.StatusCode, response);
            }
            catch (InvalidOperationException)
            {
                var error = new ErrorResponse(409, "Subject is already exists", null);
                return StatusCode(error.StatusCode, error);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateSubjectDTO subject,
            [FromQuery] Guid tenantId
        )
        {
            if (subject is null)
                return BadRequest();

            var currentSubject = _mapper.Map<Subject>(subject);
            var updatedSubject = await _service.Update(id, currentSubject, tenantId);

            if (updatedSubject is null)
                return NotFound(new ErrorResponse(404, "Subject not found", null));

            return Ok(
                new SuccessResponse<Subject>(200, "Subject updated successfully", updatedSubject)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return NotFound(new ErrorResponse(404, "Subject not found", null));

            return Ok(new SuccessResponse<bool>(200, "Subject deleted successfully", result));
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

            var response = new SuccessResponse<PaginatedResponseDTO<Subject>>(
                200,
                "Search completed successfully.",
                new PaginatedResponseDTO<Subject>(result.ToList(), size, pageNumber, pageSize)
            );

            return Ok(response);
        }

        [HttpPost("assign-requirements")]
        public async Task<IActionResult> AssignRequirements(
            [FromQuery] Guid tenantId,
            [FromBody] SubjectRequisitesDTO requisites
        )
        {
            if (!requisites.RequisitesIds.Any())
            {
                var error = new ErrorResponse(400, "Empty requirements", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.AssignRequisites(requisites, tenantId);
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

        [HttpPost("assign-programs")]
        public async Task<IActionResult> AssignPrograms(
            [FromQuery] Guid tenantId,
            [FromBody] SubjectProgramsDTO programs
        )
        {
            if (!programs.ProgramsIds.Any())
            {
                var error = new ErrorResponse(400, "Empty programs", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.AssignPrograms(programs, tenantId);
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

        [HttpDelete("revoke-requirements")]
        public async Task<IActionResult> RevokeRequirements(
            [FromQuery] Guid tenantId,
            [FromBody] SubjectRequisitesDTO requisites
        )
        {
            if (!requisites.RequisitesIds.Any())
            {
                var error = new ErrorResponse(400, "Empty requirements", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.RevokeRequisites(requisites, tenantId);
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

        [HttpDelete("revoke-programs")]
        public async Task<IActionResult> RevokePrograms(
            [FromQuery] Guid tenantId,
            [FromBody] SubjectProgramsDTO programs
        )
        {
            if (!programs.ProgramsIds.Any())
            {
                var error = new ErrorResponse(400, "Empty programs", null);
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.RevokePrograms(programs, tenantId);
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
