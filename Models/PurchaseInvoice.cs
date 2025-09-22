using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        [Required]
        public string PIId { get; set; } = string.Empty;
        [Required]
        public string SupplierName { get; set; } = string.Empty;
        [Required]
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public List<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}