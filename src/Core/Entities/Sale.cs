namespace Core.Entities;

public class Sale
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal TotalAmount { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public ICollection<SaleDetail> SaleDetails { get; set; } = null!;
    public Customer Customer { get; init; } = null!;
}