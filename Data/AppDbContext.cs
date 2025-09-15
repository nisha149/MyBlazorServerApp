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
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer
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

            // ProductGroup
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pg => pg.Id);
                entity.Property(pg => pg.Name).HasMaxLength(100).IsRequired();
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.HsnCode).HasMaxLength(20);
                entity.Property(p => p.Unit).HasMaxLength(20);
                entity.Property(p => p.RetailRate).HasPrecision(18, 4);
                entity.Property(p => p.NextPurchaseRate).HasPrecision(18, 4);
                entity.Property(p => p.Mrp).HasPrecision(10, 2);         // Added for MRP
                entity.Property(p => p.GstPercentage).HasPrecision(5, 2); // Added for GST Percentage
                entity.HasOne(p => p.ProductGroup)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(po => po.Id);
                entity.Property(po => po.POId).HasMaxLength(50).IsRequired();
                entity.Property(po => po.SupplierName).HasMaxLength(100).IsRequired();
                entity.Property(po => po.OrderDate).HasDefaultValueSql("GETDATE()");
                entity.Property(po => po.ExpectedDelivery).HasDefaultValueSql("DATEADD(day, 7, GETDATE())");
                entity.Property(po => po.TotalAmount).HasPrecision(18, 4);
                entity.Property(po => po.Status).HasMaxLength(50).IsRequired();
                entity.Property(po => po.Remarks).HasMaxLength(500);
                entity.HasMany(po => po.Items)
                    .WithOne(poi => poi.PurchaseOrder)
                    .HasForeignKey(poi => poi.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PurchaseOrderItem
            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.HasKey(poi => poi.Id);
                entity.Property(poi => poi.ProductId).HasMaxLength(36).IsRequired();
                entity.Property(poi => poi.ProductName).HasMaxLength(100).IsRequired();
                entity.Property(poi => poi.Category).HasMaxLength(100);
                entity.Property(poi => poi.TaxMode).HasMaxLength(50);
                entity.Property(poi => poi.Quantity).HasPrecision(18, 4);
                entity.Property(poi => poi.Rate).HasPrecision(18, 4);
                entity.Property(poi => poi.DiscountPercent).HasPrecision(18, 4);
                entity.Property(poi => poi.GSTPercent).HasPrecision(18, 4);
                entity.Property(poi => poi.LineTotal).HasPrecision(18, 4);
            });

            // Supplier
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.SupplierId).HasMaxLength(50).IsRequired();
                entity.Property(s => s.Name).HasMaxLength(100).IsRequired();
                entity.Property(s => s.Phone).HasMaxLength(20).IsRequired();
                entity.Property(s => s.Email).HasMaxLength(100).IsRequired();
                entity.Property(s => s.Address).HasMaxLength(500).IsRequired();
                entity.Property(s => s.Status).HasMaxLength(50).IsRequired();
                entity.Property(s => s.Notes).HasMaxLength(500);
            });
        }
    }
}