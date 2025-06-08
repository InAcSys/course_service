namespace CourseService.Domain.DTOs.ProgramLevels
{
    public class ProgramLevelsDTO
    {
        public int ProgramId { get; set; }
        public IEnumerable<int> Levels { get; set; } = new List<int>();
    }
}
