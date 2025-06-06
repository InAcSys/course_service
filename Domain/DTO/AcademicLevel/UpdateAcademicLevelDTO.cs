namespace CourseService.Domain.DTOs.AcademicLevels
{
    public class UpdateAcademicLevelDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public int AcademicProgramId { get; set; }
    }
}
