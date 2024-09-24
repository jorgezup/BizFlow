namespace Core.DTOs;

public class OrdersPaginatedRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public Guid? CustomerId { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? SortColumn { get; set; }
    public string? SortDirection { get; set; }
}