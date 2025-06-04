using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs;
using CourseService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AcademicLevelController(IService<AcademicLevel, int> academicLevelService)
        : ControllerBase
    {
        protected readonly IService<AcademicLevel, int> _academicLevelService =
            academicLevelService;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var academicLevels = await _academicLevelService.GetAll(pageNumber, pageSize);
            return Ok(academicLevels);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var academicLevel = await _academicLevelService.GetById(id);
            if (academicLevel is null)
                return NotFound();
            return Ok(academicLevel);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var academicLevel = await _academicLevelService.GetByName(name);
            if (academicLevel is null)
                return NotFound();
            return Ok(academicLevel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcademicLevelDTO academicLevel)
        {
            if (academicLevel is null)
                return BadRequest();
            var newAcademicLevel = new AcademicLevel { Name = academicLevel.Name };
            var createdAcademicLevel = await _academicLevelService.Create(newAcademicLevel);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAcademicLevel.Id },
                createdAcademicLevel
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AcademicLevelDTO academicLevel)
        {
            if (academicLevel is null)
                return BadRequest();
            var newAcademicLevel = new AcademicLevel { Id = id, Name = academicLevel.Name };
            var updatedAcademicLevel = await _academicLevelService.Update(id, newAcademicLevel);
            if (updatedAcademicLevel is null)
                return NotFound();
            return Ok(updatedAcademicLevel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedAcademicLevel = await _academicLevelService.Delete(id);
            if (!deletedAcademicLevel)
                return NotFound();
            return Ok(deletedAcademicLevel);
        }
    }
}
