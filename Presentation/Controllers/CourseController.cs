using CourseService.Application.Services.Interfaces;
using CourseService.Domain.DTOs;
using CourseService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CourseController(
        IService<Course, Guid> courseService
    ) : ControllerBase
    {
        protected readonly IService<Course, Guid> _courseService = courseService;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var courses = await _courseService.GetAll(pageNumber, pageSize);
            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _courseService.GetById(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var course = await _courseService.GetByName(name);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDTO course)
        {
            if (course is null) return BadRequest();
            var newCourse = new Course
            {
                Name = course.Name,
                ShortName = course.ShortName,
                LMSId = course.LMSId,
                GradeId = course.GradeId,
                SubjectId = course.SubjectId,
                TeacherId = course.TeacherId
            };
            var createdCourse = await _courseService.Create(newCourse);
            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseDTO course)
        {
            if (course is null) return BadRequest();
            var newCourse = new Course
            {
                Name = course.Name,
                ShortName = course.ShortName,
                LMSId = course.LMSId,
                GradeId = course.GradeId,
                SubjectId = course.SubjectId,
                TeacherId = course.TeacherId
            };
            var updatedCourse = await _courseService.Update(id, newCourse);
            if (updatedCourse is null) return NotFound();
            return Ok(updatedCourse);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _courseService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}