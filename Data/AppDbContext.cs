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
                entity.HasOne(p => p.ProductGroup)
                    .WithMany() // No inverse navigation property, so use empty WithMany
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false) // Allow null for optional category
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete if group is deleted
            });
        }
    }
}