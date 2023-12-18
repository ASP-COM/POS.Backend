using POS.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimall(6,2)")]
        public decimal TipAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PaidDate { get; set; } 
        public DateTime? PendingUntil { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }   
        public User? Customer { get; set; }
        public List<User>? Employee { get; set; }
        public List<OrderLine>? OrderLines { get; set; }
        public List<Voucher>? Vouchers { get; set; }
    }
}
