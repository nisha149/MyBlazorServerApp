using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string POId { get; set; } = string.Empty;
        [Required]
        public string SupplierName { get; set; } = string.Empty;
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ExpectedDelivery { get; set; } = DateTime.Now.AddDays(7);
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
        public List<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
        public string Remarks { get; set; } = string.Empty;
    }

    public class PurchaseOrderItem
    {
        [Key]
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; } // Foreign key
        [Required]
        public string ProductId { get; set; } = string.Empty; // Removed Guid default, handle in code if needed
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        public decimal? Quantity { get; set; } // Changed from double? to decimal? for consistency
        public decimal? Rate { get; set; } // Changed from double? to decimal?
        public decimal? DiscountPercent { get; set; } // Changed from double? to decimal?
        public decimal? GSTPercent { get; set; } // Changed from double? to decimal?
        public decimal? LineTotal { get; set; }
        [Required]
        public string TaxMode { get; set; } = string.Empty;
        public PurchaseOrder PurchaseOrder { get; set; } // Added navigation property
    }
}