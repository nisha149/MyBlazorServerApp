using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorServerApp.Models
{
    public class SalesOrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(36)]
        public string ProductId { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(100)]
        public string? Category { get; set; }
        [StringLength(50)]
        public string? TaxMode { get; set; }
        [Required, Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
        [Required, Column(TypeName = "decimal(18,4)")]
        public decimal Rate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DiscountPercent { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal GSTPercent { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal LineTotal { get; set; }
        public int SalesOrderId { get; set; }
        [ForeignKey("SalesOrderId")]
        public SalesOrder? SalesOrder { get; set; }
    }
}