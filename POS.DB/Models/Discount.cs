using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } 
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        
        // Discount can be fixed amount or in percentage 

        [Column(TypeName = "decimal(3,2)")]
        public decimal DiscountInPct { get; set; }
        
        [Column(TypeName = "decimal(6,2)")]
        public decimal DiscountInAmount { get; set; }
        public int? minQuantity { get; set; } // Optional: Min quantity of products for discount to be applied

        // Discount can be set for specific item, categories or loyalty programs
        public Item? ForSpecificItem { get; set; }
        public List<ItemCategory>? ForCategories { get; set; }

        public List<LoyaltyProgram>? ForLoyaltyPrograms { get; set; }
    }
}
