using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<PriceHistory> PriceHistories { get; init; }
    public DbSet<CustomerPreferences> CustomerPreferences { get; init; }

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
        
        // CustomerPreferences configuration
        modelBuilder.Entity<CustomerPreferences>()
            .HasKey(p => p.Id);
        
        // CustomerPreferences configuration
        // One-to-many relationship between Customer and CustomerPreferences
        // Customer has many CustomerPreferences
        modelBuilder.Entity<CustomerPreferences>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // CustomerPreferences configuration
        // One-to-many relationship between Product and CustomerPreferences
        // Product has many CustomerPreferences
        modelBuilder.Entity<CustomerPreferences>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}