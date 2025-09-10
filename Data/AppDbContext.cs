using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Models;

namespace MyBlazorServerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasQueryFilter(c => !c.IsDeleted);
                entity.Property(c => c.Code).HasMaxLength(20).IsRequired();
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Gstin).HasMaxLength(20);
                entity.Property(c => c.IsDeleted).HasDefaultValue(false);
            });

            // ProductGroup configuration
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pg => pg.Id);
                entity.Property(pg => pg.Name).HasMaxLength(100).IsRequired();
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.HsnCode).HasMaxLength(20);
                entity.Property(p => p.Unit).HasMaxLength(20);
                entity.Property(p => p.RetailRate).HasPrecision(18, 4);
                entity.Property(p => p.NextPurchaseRate).HasPrecision(18, 4);
                entity.HasOne(p => p.ProductGroup)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PurchaseOrder configuration
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(po => po.Id);
                entity.Property(po => po.POId).HasMaxLength(50).IsRequired();
                entity.Property(po => po.SupplierName).HasMaxLength(100).IsRequired();
                entity.Property(po => po.OrderDate).HasDefaultValueSql("GETDATE()");
                entity.Property(po => po.ExpectedDelivery).HasDefaultValueSql("DATEADD(day, 7, GETDATE())");
                entity.Property(po => po.TotalAmount).HasPrecision(18, 4);
                entity.Property(po => po.Status).HasMaxLength(50).IsRequired();
            });

        }
    }
}