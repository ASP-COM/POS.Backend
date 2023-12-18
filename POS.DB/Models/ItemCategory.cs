using System.ComponentModel.DataAnnotations;

namespace POS.DB.Models
{
    public class ItemCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Business? Business { get; set; }
        public List<Item> Items { get; set; }
        public List<Discount>? Discounts { get; set; }
    }
}
