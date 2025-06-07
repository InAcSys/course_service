namespace CourseService.Domain.DTOs.CourseSubjects
{
    public class CourseSubjectsDTO
    {
        public Guid CourseId { get; set; }
        public IEnumerable<Guid> SubjectsIds { get; set; } = new List<Guid>();
    }
}
