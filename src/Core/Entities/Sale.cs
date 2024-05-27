namespace Core.Entities;

public class Sale
{
    public Guid SaleId { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.Now;
    public List<SaleItem> SaleItems { get; set; } = [];
}