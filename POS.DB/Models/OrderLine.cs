using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    [PrimaryKey(nameof(OrderId), nameof(LineId))]
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int LineId { get; set;}

        [Column(TypeName = "decimal(6,2)")]
        public decimal UnitPrice { get; set; }

        public int UnitCount { get; set; }

        public Item Item { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }

        public Discount? Discount { get; set; }

        [ForeignKey("AppliedTaxId")]
        public int AppliedTaxId { get; set; }   

        public Tax AppliedTax { get; set; }
        public Order Order { get; set; }

    }
}
