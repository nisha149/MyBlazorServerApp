using System.ComponentModel.DataAnnotations;

namespace MyBlazorServerApp.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}