// Models/PurchaseInvoice.cs
using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }

        // Auto-generated → NO [Required]
        public string? PIId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        // Set in code from SupplierId → NO [Required]
        public string? SupplierName { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Safe default
        public List<PurchaseInvoiceItem> Items { get; set; } = new();
    }
}