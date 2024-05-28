namespace Core.Entities;

public class Sale (Guid saleId, Guid customerId, DateTime saleDate, Customer customer, List<SaleItem> saleItems)
{
    public Guid SaleId { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = customer;
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public List<SaleItem> SaleItems { get; set; } = saleItems;
}