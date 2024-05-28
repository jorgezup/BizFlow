namespace Core.Entities;

public class Customer(
    Guid customerId,
    string name,
    string email,
    string phoneNumber,
    string address,
    List<DayOfWeek> preferredDay,
    List<Product> preferredProducts,
    List<Sale> sales)
{
    public Guid CustomerId { get; set; } = customerId;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string Address { get; set; } = address;
    public List<DayOfWeek> PreferredDay { get; set; } = preferredDay;
    public List<Product> PreferredProducts { get; set; } = preferredProducts;
    public List<Sale> Sales { get; set; } = sales;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}