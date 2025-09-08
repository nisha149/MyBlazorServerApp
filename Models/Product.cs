using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string HsnCode { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal NextPurchaseRate { get; set; }
        public decimal GstPercentage { get; set; }
        public int StockQuantity { get; set; }
        public decimal Mrp { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal RetailRate { get; set; }
        public int ReorderLevel { get; set; }
        public ProductGroup? ProductGroup { get; set; } 
    }
}