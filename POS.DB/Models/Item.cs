using POS.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // New property for the amount of time the service will take
        public TimeSpan? ServiceDuration { get; set; }

        public int DefaultTaxId { get; set; }

        [ForeignKey("DefaultTaxId")]

        public Tax DefaultTax { get; set; }

        public int BusinessId { get; set; }

        [ForeignKey("BusinessId")]

        public Business Business { get; set; }
        public List<ItemCategory> Categories { get; set; }

    }
}
