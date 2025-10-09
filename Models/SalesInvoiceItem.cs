namespace MyBlazorServerApp.Models;

public class SalesInvoiceItem
{
    public int Id { get; set; }
    public int SalesInvoiceId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal? DiscountPercent { get; set; }
    public decimal? GSTPercent { get; set; }
    public string? TaxMode { get; set; }
    public decimal LineTotal { get; set; }
}