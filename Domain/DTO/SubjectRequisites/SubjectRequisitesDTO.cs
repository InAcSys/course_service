namespace CourseService.Domain.DTOs.SubjectRequisites
{
    public class SubjectRequisitesDTO
    {
        public Guid SubjectId { get; set; }
        public IEnumerable<Guid> RequisitesIds { get; set; } = new List<Guid>();
    }
}
