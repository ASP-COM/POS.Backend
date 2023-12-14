using POS.DB.Enums;
using System.ComponentModel.DataAnnotations;

namespace POS.DB.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } // WithoutTax
        public bool IsUnavailable { get; set; }
        public ItemType Type { get; set; }

        public Tax DefaultTax { get; set; }
        public Business Business { get; set; }
        public List<ItemCategory> Categories { get; set; }

    }
}
