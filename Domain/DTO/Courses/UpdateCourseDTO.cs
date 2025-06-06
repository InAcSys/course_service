namespace CourseService.Domain.DTOs.Courses
{
    public class UpdateCourseDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Code { get; set; } = "";
        public int AcademicLevelId { get; set; }
    }
}
