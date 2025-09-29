namespace MyBlazorServerApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string? HsnCode { get; set; }
        public string? Unit { get; set; }
        public decimal? NextPurchaseRate { get; set; }
        public decimal? GstPercentage { get; set; }
        public int StockQuantity { get; set; } = 0;
        public decimal? Mrp { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? RetailRate { get; set; }
        public int ReorderLevel { get; set; } = 0;
        public ProductGroup? ProductGroup { get; set; }
    }
}