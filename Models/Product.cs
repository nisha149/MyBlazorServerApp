namespace MyBlazorServerApp.Models
{
    public class Product
    {
        public int Id { get; set; } // Identity column
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? HsnCode { get; set; }
        public string? Unit { get; set; }
        public decimal NextPurchaseRate { get; set; }
        public decimal? GstPercentage { get; set; }
        public int StockQuantity { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal RetailRate { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsDeleted { get; set; }
        public ProductGroup? ProductGroup { get; set; }
    }
}