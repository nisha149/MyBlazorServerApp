using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(20)]
        public string SupplierId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active";

        public bool IsDeleted { get; set; } = false;

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
