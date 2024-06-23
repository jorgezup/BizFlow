using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);

        // Product configuration
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);
    }
}