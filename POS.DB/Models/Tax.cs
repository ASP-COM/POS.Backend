using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DB.Models
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AmountPct{ get; set; }

        public List<Item>? Items { get; set; }
        public List<OrderLine>? OrderLines { get; set; }
    }
}
