using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    [PrimaryKey(nameof(OrderId), nameof(LineId))]
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int LineId { get; set;}

        [Column(TypeName = "decimall(6,2)")]
        public decimal Price { get; set; }

        public Item Item { get; set; }

        public Discount? Discount { get; set; }

        public Tax AppliedTax { get; set; }
        public Order Order { get; set; }

    }
}
