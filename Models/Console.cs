using System.ComponentModel.DataAnnotations;

namespace assignments.Models
{//good
    public class Console
    {
        [Key]
        public int ConsoleId { get; set; } = 0;

        [Required, StringLength(300)]
        public string Title { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = String.Empty;

        [Required]
        public decimal Price { get; set; } = 0.0M;

        
        public virtual ICollection<Product>? Products { get; set; }

    }
}