using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }
        public virtual DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- SEQUENCES ---
            // Add a DB sequence for generating unique customer codes (CUST###).
            // This will be created by migrations OR you can create it manually via SQL.
            modelBuilder.HasSequence<int>("CustomerCodeSeq", schema: "dbo")
                        .StartsAt(1)
                        .IncrementsBy(1);

            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(c => !c.IsDeleted);
                entity.HasIndex(c => c.Code).IsUnique();
                entity.Property(c => c.Code).HasMaxLength(20).IsRequired();
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Gstin).HasMaxLength(20);
                entity.Property(c => c.Address).HasMaxLength(500);
                entity.Property(c => c.Email).HasMaxLength(100);
            });

            // ProductGroup
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("ProductGroups");
                entity.HasKey(pg => pg.Id);
                entity.Property(pg => pg.Id).ValueGeneratedOnAdd();
                entity.Property(pg => pg.Name).HasMaxLength(100).IsRequired();
                entity.Property(pg => pg.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(pg => !pg.IsDeleted);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.HsnCode).HasMaxLength(20);
                entity.Property(p => p.Unit).HasMaxLength(20);
                entity.Property(p => p.RetailRate).HasPrecision(18, 4);
                entity.Property(p => p.NextPurchaseRate).HasPrecision(18, 4);
                entity.Property(p => p.Mrp).HasPrecision(10, 2);
                entity.Property(p => p.GstPercentage).HasPrecision(5, 2);
                entity.Property(p => p.StockQuantity).HasDefaultValue(0);
                entity.Property(p => p.DiscountPercentage).HasPrecision(5, 2);
                entity.Property(p => p.ReorderLevel).HasDefaultValue(0);
                entity.Property(p => p.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(p => !p.IsDeleted);
                entity.HasOne(p => p.ProductGroup)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrders");
                entity.HasKey(po => po.Id);
                entity.Property(po => po.Id).ValueGeneratedOnAdd();
                entity.Property(po => po.POId).HasMaxLength(50).IsRequired();
                entity.HasIndex(po => po.POId).IsUnique();
                entity.Property(po => po.SupplierName).HasMaxLength(100).IsRequired();
                entity.Property(po => po.OrderDate).HasDefaultValueSql("GETDATE()");
                entity.Property(po => po.ExpectedDelivery).HasDefaultValueSql("DATEADD(day, 7, GETDATE())");
                entity.Property(po => po.TotalAmount).HasPrecision(18, 4);
                entity.Property(po => po.Status).HasMaxLength(50).IsRequired();
                entity.Property(po => po.Remarks).HasMaxLength(500);
                entity.Property(po => po.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(po => !po.IsDeleted);
                entity.HasMany(po => po.Items)
                    .WithOne(poi => poi.PurchaseOrder)
                    .HasForeignKey(poi => poi.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PurchaseOrderItem
            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("PurchaseOrderItems");
                entity.HasKey(poi => poi.Id);
                entity.Property(poi => poi.Id).ValueGeneratedOnAdd();
                // removed HasConversion<int>() so ProductId stays string in DB
                entity.Property(poi => poi.ProductId).IsRequired();
                entity.Property(poi => poi.ProductName).HasMaxLength(100).IsRequired();
                entity.Property(poi => poi.TaxMode).HasMaxLength(50);
                entity.Property(poi => poi.Quantity).HasPrecision(18, 4).IsRequired();
                entity.Property(poi => poi.Rate).HasPrecision(18, 4).IsRequired();
                entity.Property(poi => poi.DiscountPercent).HasPrecision(18, 4);
                entity.Property(poi => poi.GSTPercent).HasPrecision(18, 4);
                entity.Property(poi => poi.LineTotal).HasPrecision(18, 4);
            });

            // Supplier
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.SupplierId).HasMaxLength(50).IsRequired();
                entity.HasIndex(s => s.SupplierId).IsUnique();
                entity.Property(s => s.Name).HasMaxLength(100).IsRequired();
                entity.Property(s => s.Phone).HasMaxLength(20).IsRequired(false);
                entity.Property(s => s.Email).HasMaxLength(100).IsRequired(false);
                entity.Property(s => s.Address).HasMaxLength(500).IsRequired(false);
                entity.Property(s => s.Status).HasMaxLength(50).IsRequired();
                entity.Property(s => s.Notes).HasMaxLength(500);
                entity.Property(s => s.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(s => !s.IsDeleted);
            });

            // PurchaseInvoice
            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.ToTable("PurchaseInvoices");
                entity.HasKey(pi => pi.Id);
                entity.Property(pi => pi.Id).ValueGeneratedOnAdd();
                entity.Property(pi => pi.PIId).HasMaxLength(50).IsRequired();
                entity.HasIndex(pi => pi.PIId).IsUnique();
                entity.Property(pi => pi.SupplierName).HasMaxLength(100).IsRequired();
                entity.Property(pi => pi.InvoiceDate).HasDefaultValueSql("GETDATE()");
                entity.Property(pi => pi.DueDate).HasDefaultValueSql("DATEADD(day, 30, GETDATE())");
                entity.Property(pi => pi.TotalAmount).HasPrecision(18, 4);
                entity.Property(pi => pi.Status).HasMaxLength(50).IsRequired();
                entity.Property(pi => pi.Remarks).HasMaxLength(500);
                entity.Property(pi => pi.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(pi => !pi.IsDeleted);
                entity.HasMany(pi => pi.Items)
                    .WithOne(pii => pii.PurchaseInvoice)
                    .HasForeignKey(pii => pii.PurchaseInvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PurchaseInvoiceItem
            modelBuilder.Entity<PurchaseInvoiceItem>(entity =>
            {
                entity.ToTable("PurchaseInvoiceItems");
                entity.HasKey(pii => pii.Id);
                entity.Property(pii => pii.Id).ValueGeneratedOnAdd();
                // removed HasConversion<int>() so ProductId stays string
                entity.Property(pii => pii.ProductId).IsRequired();
                entity.Property(pii => pii.ProductName).HasMaxLength(100).IsRequired();
                entity.Property(pii => pii.TaxMode).HasMaxLength(50);
                entity.Property(pii => pii.Quantity).HasPrecision(18, 4).IsRequired();
                entity.Property(pii => pii.Rate).HasPrecision(18, 4).IsRequired();
                entity.Property(pii => pii.DiscountPercent).HasPrecision(18, 4);
                entity.Property(pii => pii.GSTPercent).HasPrecision(18, 4);
                entity.Property(pii => pii.LineTotal).HasPrecision(18, 4);
            });

            // SalesOrder
            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("SalesOrders");
                entity.HasKey(so => so.Id);
                entity.Property(so => so.Id).ValueGeneratedOnAdd();
                entity.Property(so => so.SOId).HasMaxLength(50).IsRequired();
                entity.HasIndex(so => so.SOId).IsUnique();
                entity.Property(so => so.CustomerName).HasMaxLength(100).IsRequired();
                entity.Property(so => so.OrderDate).IsRequired();
                entity.Property(so => so.ExpectedDelivery).IsRequired(false);
                entity.Property(so => so.TotalAmount).HasPrecision(18, 4);
                entity.Property(so => so.Status).HasMaxLength(50).IsRequired();
                entity.Property(so => so.Remarks).HasMaxLength(500);
                entity.Property(so => so.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(so => !so.IsDeleted);
                entity.HasOne(so => so.Customer)
                    .WithMany()
                    .HasForeignKey(so => so.CustomerId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(so => so.Items)
                    .WithOne(soi => soi.SalesOrder)
                    .HasForeignKey(soi => soi.SalesOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // SalesOrderItem
            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.ToTable("SalesOrderItems");
                entity.HasKey(soi => soi.Id);
                entity.Property(soi => soi.Id).ValueGeneratedOnAdd();
                // removed HasConversion<int>() so ProductId stays string (if your model has string)
                entity.Property(soi => soi.ProductId);
                entity.Property(soi => soi.ProductName).HasMaxLength(100).IsRequired();
                entity.Property(soi => soi.TaxMode).HasMaxLength(50);
                entity.Property(soi => soi.Quantity).HasPrecision(18, 4).IsRequired();
                entity.Property(soi => soi.Rate).HasPrecision(18, 4).IsRequired();
                entity.Property(soi => soi.DiscountPercent).HasPrecision(18, 4);
                entity.Property(soi => soi.GSTPercent).HasPrecision(18, 4);
                entity.Property(soi => soi.LineTotal).HasPrecision(18, 4);
            });

            // SalesInvoice
            modelBuilder.Entity<SalesInvoice>(entity =>
            {
                entity.ToTable("SalesInvoices");
                entity.HasKey(si => si.Id);
                entity.Property(si => si.Id).ValueGeneratedOnAdd();
                entity.Property(si => si.InvoiceId).HasMaxLength(50).IsRequired();
                entity.HasIndex(si => si.InvoiceId).IsUnique();
                entity.Property(si => si.CustomerName).HasMaxLength(100);
                entity.Property(si => si.InvoiceDate).IsRequired();
                entity.Property(si => si.DueDate).IsRequired(false);
                entity.Property(si => si.TotalAmount).HasPrecision(18, 4);
                entity.Property(si => si.Status).HasMaxLength(50);
                entity.Property(si => si.Remarks).HasMaxLength(500);
                entity.Property(si => si.IsDeleted).HasDefaultValue(false);
                entity.HasQueryFilter(si => !si.IsDeleted);
                entity.Property(si => si.CustomerId).HasConversion<int?>().IsRequired(false);
                entity.HasOne(si => si.Customer)
                    .WithMany()
                    .HasForeignKey(si => si.CustomerId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(si => si.Items)
                    .WithOne()
                    .HasForeignKey(sii => sii.SalesInvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // SalesInvoiceItem
            modelBuilder.Entity<SalesInvoiceItem>(entity =>
            {
                entity.ToTable("SalesInvoiceItems");
                entity.HasKey(sii => sii.Id);
                entity.Property(sii => sii.Id).ValueGeneratedOnAdd();
                // removed HasConversion<int>() so ProductId stays string
                entity.Property(sii => sii.ProductId).IsRequired();
                entity.Property(sii => sii.ProductName).HasMaxLength(100).IsRequired();
                entity.Property(sii => sii.Category).HasMaxLength(100);
                entity.Property(sii => sii.TaxMode).HasMaxLength(50);
                entity.Property(sii => sii.Quantity).HasPrecision(18, 4).IsRequired();
                entity.Property(sii => sii.Rate).HasPrecision(18, 4).IsRequired();
                entity.Property(sii => sii.DiscountPercent).HasPrecision(18, 4);
                entity.Property(sii => sii.GSTPercent).HasPrecision(18, 4);
                entity.Property(sii => sii.LineTotal).HasPrecision(18, 4).IsRequired();
                entity.Property(sii => sii.SalesInvoiceId).IsRequired();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Console.WriteLine("Attempting to save changes to database...");
                var result = await base.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"Successfully saved {result} changes to database.");
                return result;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error: {ex.Message} - Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message} - Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                Console.WriteLine("Attempting to save changes to database (synchronous)...");
                var result = base.SaveChanges();
                Console.WriteLine($"Successfully saved {result} changes to database.");
                return result;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error: {ex.Message} - Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message} - Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
