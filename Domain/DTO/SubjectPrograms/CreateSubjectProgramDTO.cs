namespace CourseService.Domain.DTOs.SubjectPrograms
{
    public class CreateSubjectProgramDTO
    {
        public Guid SubjectId { get; set; }
        public IEnumerable<int> AcademicProgramIds { get; set; } = new List<int>();
    }
}
