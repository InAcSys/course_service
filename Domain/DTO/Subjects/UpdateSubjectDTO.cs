namespace CourseService.Domain.DTOs.Subjects
{
    public class UpdateSubjectDTO
    {
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Code { get; set; }
        public int? LMSId { get; set; }
        public Guid? CourseId { get; set; }
    }
}
