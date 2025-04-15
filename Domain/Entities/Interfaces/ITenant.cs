namespace CourseService.Domain.Entities.Interfaces
{
    public interface ITenant
    {
        Guid TenantId { get; set; }
    }
}