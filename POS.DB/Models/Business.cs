using System.ComponentModel.DataAnnotations;

namespace POS.DB.Models
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Voucher>? Vouchers { get; set; }
        public List<Item>? Items { get; set; }
        public List<ItemCategory>? ItemCategories { get; set; }
        public List<User>? Users { get; set; }   

    }
}
