using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<PriceHistory> PriceHistories { get; init; }
    public DbSet<CustomerPreferences> CustomerPreferences { get; init; }
    public DbSet<OrderDetail> OrderDetails { get; init; }
    public DbSet<Payment> Payments { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderLifeCycle> OrderLifeCycles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);

        // One-to-many relationship between Customer and Order 
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders) // Alterando a relação para one-to-many
            .HasForeignKey(o => o.CustomerId);
        // .OnDelete(DeleteBehavior.Cascade);

        // Product configuration
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        // PriceHistory configuration
        modelBuilder.Entity<PriceHistory>()
            .HasKey(p => p.Id);

        // One-to-many relationship between Product and PriceHistory
        modelBuilder.Entity<PriceHistory>()
            .HasOne(p => p.Product)
            .WithMany(b => b.PriceHistories)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // CustomerPreferences configuration
        modelBuilder.Entity<CustomerPreferences>()
            .HasKey(p => p.Id);

        // One-to-many relationship between Customer and CustomerPreferences
        modelBuilder.Entity<CustomerPreferences>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.CustomerPreferences)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-many relationship between Product and CustomerPreferences
        modelBuilder.Entity<CustomerPreferences>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-many relationship between Order and OrderDetail (one-way)
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent?.SetField(null);  // Remove a navegação inversa

        // One-to-many relationship between Product and OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Payment configuration
        modelBuilder.Entity<Payment>()
            .HasKey(p => p.Id);

        // One-to-one relationship between Order and Payment (one-way)
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent?.SetField(null);  // Remove a navegação inversa

        // One-to-many relationship between Order and OrderLifeCycle (one-way)
        modelBuilder.Entity<OrderLifeCycle>()
            .HasOne(olc => olc.Order)
            .WithMany(o => o.OrderLifeCycle)
            .HasForeignKey(olc => olc.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent?.SetField(null);  // Remove a navegação inversa

    }
}