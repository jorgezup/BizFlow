using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Sale> Sales { get; init; }
    public DbSet<SaleItem> SaleItems { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);

        // modelBuilder.Entity<Customer>()
        //     .HasMany(c => c.Sales)
        //     .WithOne(s => s.Customer)
        //     .HasForeignKey(s => s.CustomerId);

        // Product configuration
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        // modelBuilder.Entity<Product>()
        //     .HasMany(p => p.SaleItems)
        //     .WithOne(si => si.Product)
        //     .HasForeignKey(si => si.ProductId);

        // Sale configuration
        modelBuilder.Entity<Sale>()
            .HasKey(s => s.SaleId);

        // modelBuilder.Entity<Sale>()
        //     .HasMany(s => s.SaleItems)
        //     .WithOne(si => si.Sale)
        //     .HasForeignKey(si => si.SaleId);

        // SaleItem configuration 
        modelBuilder.Entity<SaleItem>()
            .HasKey(si => si.SaleItemId); 
    }
}