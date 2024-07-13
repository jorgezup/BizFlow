using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<PriceHistory> PriceHistories { get; init; }
    public DbSet<CustomerPreferences> CustomerPreferences { get; init; }
    public DbSet<Sale> Sales { get; init; }
    public DbSet<SaleDetail> SaleDetails { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);

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
            .WithMany(p => p.CustomerPreferences)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Sale configuration
        modelBuilder.Entity<Sale>()
            .HasKey(s => s.Id);

        // SaleDetail configuration
        modelBuilder.Entity<SaleDetail>()
            .HasKey(s => s.Id);

        // One-to-many relationship between Sale and SaleDetail
        modelBuilder.Entity<SaleDetail>()
            .HasOne(s => s.Sale)
            .WithMany(b => b.SaleDetails)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-many relationship between Product and SaleDetail
        modelBuilder.Entity<SaleDetail>()
            .HasOne(s => s.Product)
            .WithMany(p => p.SaleDetails)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}