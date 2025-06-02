using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs;
using CourseService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AcademicProgramController(
        IService<AcademicProgram, int> academicProgramService
    ) : ControllerBase
    {
        protected readonly IService<AcademicProgram, int> _academicProgramService = academicProgramService;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var academicPrograms = await _academicProgramService.GetAll(pageNumber, pageSize);
            return Ok(academicPrograms);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var academicProgram = await _academicProgramService.GetById(id);
            if (academicProgram is null) return NotFound();
            return Ok(academicProgram);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var academicProgram = await _academicProgramService.GetByName(name);
            if (academicProgram is null) return NotFound();
            return Ok(academicProgram);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcademicProgramDTO academicProgram)
        {
            if (academicProgram is null) return BadRequest();
            var newAcademicProgram = new AcademicProgram
            {
                Name = academicProgram.Name,
            };
            var createdAcademicProgram = await _academicProgramService.Create(newAcademicProgram);
            return CreatedAtAction(nameof(GetById), new { id = createdAcademicProgram.Id }, createdAcademicProgram);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AcademicProgramDTO academicProgram)
        {
            if (academicProgram is null) return BadRequest();
            var newAcademicProgram = new AcademicProgram
            {
                Id = id,
                Name = academicProgram.Name,
            };
            var updatedAcademicProgram = await _academicProgramService.Update(id, newAcademicProgram);
            if (updatedAcademicProgram is null) return NotFound();
            return Ok(updatedAcademicProgram);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedAcademicProgram = await _academicProgramService.Delete(id);
            if (!deletedAcademicProgram) return NotFound();
            return Ok(deletedAcademicProgram);
        }
    }
}