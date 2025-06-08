using CourseService.Domain.Entities.Abstracts;

namespace CourseService.Domain.Entities.Concretes
{
    public class AcademicProgram : MainEntity<int>
    {
        public string DegreeType { get; set; } = "";
        public string DurationType { get; set; } = "";
        public int Periods { get; set; } = 0;
    }
}
