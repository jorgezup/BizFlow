namespace Application.DTOs.OrderDetail;

public class OrderDetailRequest
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}

public class OrderDetailRequestToSale
{
    public Guid ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Subtotal { get; set; }
}

    
    