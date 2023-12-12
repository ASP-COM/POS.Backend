using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    [PrimaryKey(nameof(Order_Id), nameof(Line_Id))]
    public class OrderLine
    {
        public int Order_Id { get; set; }
        public int Line_Id { get; set;}

        [Column(TypeName = "decimall(6,2)")]
        public decimal Price { get; set; }

        public Item Item { get; set; }

        public Discount Discount { get; set; }

        public Tax AppliedTax { get; set; }
        public Order Order { get; set; }

    }
}
