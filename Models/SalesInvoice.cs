namespace MyBlazorServerApp.Models;

public class SalesInvoice
{
    public int Id { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public string? Remarks { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public bool IsDeleted { get; set; }
    public List<SalesInvoiceItem> Items { get; set; } = new();
}