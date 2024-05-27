using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Entities
    public DbSet<Customer?> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Sale>().ToTable("Sale");
        modelBuilder.Entity<SaleItem>().ToTable("SaleItem");

        modelBuilder.Entity<Customer>().Property(x => x.Name).HasColumnType("varchar(200)");
        modelBuilder.Entity<Customer>().Property(x => x.Email).HasColumnType("varchar(200)");
        modelBuilder.Entity<Customer>().Property(x => x.Address).HasColumnType("varchar(200)");
        modelBuilder.Entity<Customer>().Property(x => x.PhoneNumber).HasColumnType("varchar(200)");

        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("varchar(200)");
        modelBuilder.Entity<Product>().Property(x => x.Category).HasColumnType("varchar(200)");
        modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<SaleItem>()
            .HasOne(x => x.Product)
            .WithMany(x => x.SaleItems)
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(x => x.Sale)
            .WithMany(x => x.SaleItems)
            .HasForeignKey(x => x.SaleId);
    }
}