using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<PriceHistory> PriceHistories { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);

        // Product configuration
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);
        
        // PriceHistory configuration
        modelBuilder.Entity<PriceHistory>()
            .HasKey(p => p.Id);
        
        // PriceHistory configuration
        // One-to-many relationship between Product and PriceHistory
        // Product has many PriceHistories
        modelBuilder.Entity<PriceHistory>()
            .HasOne(p => p.Product)
            .WithMany(b => b.PriceHistories)
            .HasForeignKey(p => p.ProductId);
    }
}