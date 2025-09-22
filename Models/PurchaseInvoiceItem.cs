using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class PurchaseInvoiceItem
    {
        public int Id { get; set; } // Primary key
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Category { get; set; }
        public int? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? GSTPercent { get; set; }
        public string? TaxMode { get; set; }
        public decimal? LineTotal { get; set; }
        public int? PurchaseInvoiceId { get; set; } // Foreign key
        public PurchaseInvoice? PurchaseInvoice { get; set; } // Navigation property
    }
}