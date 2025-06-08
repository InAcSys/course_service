namespace CourseService.Domain.DTOs.ProgramSubjects
{
    public class ProgramSubjectsDTO
    {
        public int ProgramId { get; set; }
        public IEnumerable<Guid> Subjects { get; set; } = new List<Guid>();
    }
}
