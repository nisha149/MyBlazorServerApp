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
    }
}