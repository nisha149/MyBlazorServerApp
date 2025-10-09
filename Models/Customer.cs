using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; // needed for [Index]

namespace MyBlazorServerApp.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Customer
    {
        [Key] // optional (EF infers Id as key automatically)
        public int Id { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Customer ID")]
        public string Code { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Phone, StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(20)]
        [Display(Name = "GSTIN")]
        public string? Gstin { get; set; }
        public string? Address { get; set; }
        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        public bool IsDeleted { get; set; } = false; // Added for soft delete
    }
}