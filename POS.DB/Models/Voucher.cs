using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsUsed { get; set; }

        [Column(TypeName = "decimall(6,2)")]
        public decimal Amount { get; set; }
        public Order? Order { get; set; }
        public Business Business { get; set; }
    }
}
