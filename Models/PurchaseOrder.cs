using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string POId { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string SupplierName { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public decimal? TotalAmount { get; set; }
        [Required, StringLength(20)]
        public string Status { get; set; } = "Pending";
        [StringLength(500)]
        public string? Remarks { get; set; }
        [StringLength(200)]
        public string? SupplierAddress { get; set; }
        [StringLength(20)]
        public string? SupplierPhone { get; set; }
        [StringLength(100)]
        public string? SupplierEmail { get; set; }
        public virtual List<PurchaseOrderItem> Items { get; set; } = new();
    }

    public class PurchaseOrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductId { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Category { get; set; }
        public int? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? GSTPercent { get; set; }
        [StringLength(20)]
        public string? TaxMode { get; set; }
        public decimal? LineTotal { get; set; }
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
    }
}