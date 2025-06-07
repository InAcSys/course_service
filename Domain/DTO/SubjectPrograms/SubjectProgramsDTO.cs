namespace CourseService.Domain.DTOs.SubjectRequisites
{
    public class SubjectProgramsDTO
    {
        public Guid SubjectId { get; set; }
        public IEnumerable<int> ProgramsIds { get; set; } = new List<int>();
    }
}
