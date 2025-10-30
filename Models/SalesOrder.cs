using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyBlazorServerApp.Models
{
    [Index(nameof(SOId), IsUnique = true)]
    public class SalesOrder
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string SOId { get; set; } = null!;

        [StringLength(100)]
        public string? CustomerName { get; set; }   // ✅ Made optional

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ExpectedDelivery { get; set; }

        [Precision(18, 4)]
        public decimal TotalAmount { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = null!;

        [StringLength(500)]
        public string? Remarks { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public bool IsDeleted { get; set; }

        public List<SalesOrderItem> Items { get; set; } = new();
    }
}
