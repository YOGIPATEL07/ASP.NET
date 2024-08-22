using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignments.Models
{
    public class Product
    {
        [Key]
        public int SKU { get; set; } = 0;

        [Required]
        [Display(Name = "Console")]
        public int GamesId { get; set; } = 0;

        [Required, StringLength(300)]
        public string ProductName { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? ProductDescription { get; set; } = String.Empty;

        [StringLength(250)]
        public string? ProductImage { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0.01M;

        [ForeignKey("GamesId")]
        public virtual Console? Game { get; set; }

    }
}