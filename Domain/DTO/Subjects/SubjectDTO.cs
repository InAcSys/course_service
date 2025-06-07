namespace CourseService.Domain.DTOs.Subjects
{
    public class SubjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public int Credits { get; set; }
        public int LMSId { get; set; }
        public int AcademicLevelId { get; set; }
        public Guid TeacherId { get; set; }
        public IEnumerable<int> AcademicProgramIds { get; set; } = new List<int>();
        public IEnumerable<Guid> RequirementIds { get; set; } = new List<Guid>();
        public Guid TenantId { get; set; }
    }
}
