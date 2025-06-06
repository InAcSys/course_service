namespace CourseService.Domain.DTOs.Subjects
{
    public class UpdateSubjectDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public int Credits { get; set; }
        public int LMSId { get; set; }
        public int AcademicLevelId { get; set; }
    }
}
