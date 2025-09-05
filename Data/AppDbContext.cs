using Microsoft.EntityFrameworkCore;
using MyBlazorServerApp.Models;

namespace MyBlazorServerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Code).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Phone).HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Gstin).HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(c => c.IsDeleted).HasDefaultValue(false);
        }
    }
}
