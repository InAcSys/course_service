namespace CourseService.Domain.DTOs.AcademicPrograms
{
    public class CreateAcademicProgramDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public string DegreeType { get; set; } = "";
        public string DurationType { get; set; } = "";
        public int Periods { get; set; } = 0;
    }
}
