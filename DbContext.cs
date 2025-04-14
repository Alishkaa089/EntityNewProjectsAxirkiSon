using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class StepShopDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Invoice> Invoices { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=STHQ012E-14;Initial Catalog=StepShops;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Invoice>().ToTable("Invoice");
        modelBuilder.Entity<Order>().ToTable("Order");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(o => o.Invoice)
                .WithMany(i => i.Orders)
                .HasForeignKey(o => o.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasMany(i => i.Orders)
                .WithOne(o => o.Invoice)
                .HasForeignKey(o => o.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(i => i.CashierId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }


}