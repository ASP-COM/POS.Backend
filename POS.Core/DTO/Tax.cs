using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Core.DTO
{
    public class Tax
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public decimal AmountPct { get; set; }

        public static explicit operator Tax(DB.Models.Tax t) => new Tax
        {
            Id = t.Id,
            Description = t.Description,
            AmountPct = t.AmountPct,
        };
    }
}
