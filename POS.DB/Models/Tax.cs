using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace POS.DB.Models
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal AmountPct{ get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; set; }

        [JsonIgnore]
        public List<OrderLine>? OrderLines { get; set; }
    }
}
