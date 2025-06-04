using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs;
using CourseService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SubjectController(IService<Subject, int> subjectService) : ControllerBase
    {
        protected readonly IService<Subject, int> _subjectService = subjectService;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var subjects = await _subjectService.GetAll(pageNumber, pageSize);
            return Ok(subjects);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _subjectService.GetById(id);
            if (subject is null)
                return NotFound();
            return Ok(subject);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var subject = await _subjectService.GetByName(name);
            if (subject is null)
                return NotFound();
            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectDTO subject)
        {
            if (subject is null)
                return BadRequest();
            var newSubject = new Subject { Name = subject.Name };
            var createdSubject = await _subjectService.Create(newSubject);
            return CreatedAtAction(nameof(GetById), new { id = createdSubject.Id }, createdSubject);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubjectDTO subject)
        {
            if (subject is null)
                return BadRequest();
            var newSubject = new Subject { Id = id, Name = subject.Name };
            var updatedSubject = await _subjectService.Update(id, newSubject);
            if (updatedSubject is null)
                return NotFound();
            return Ok(updatedSubject);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedSubject = await _subjectService.Delete(id);
            if (!deletedSubject)
                return NotFound();
            return Ok(deletedSubject);
        }
    }
}
