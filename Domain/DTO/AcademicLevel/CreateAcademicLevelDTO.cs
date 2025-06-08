namespace CourseService.Domain.DTOs.AcademicLevels
{
    public class CreateAcademicLevelDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public int Order { get; set; }
    }
}
