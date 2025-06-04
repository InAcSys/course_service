namespace CourseService.Domain.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; } = "";
        public string ShortName { get; set; } = "";
        public int LMSId { get; set; }
        public int GradeId { get; set; }
        public int SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
