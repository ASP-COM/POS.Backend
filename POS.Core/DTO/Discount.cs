using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Core.DTO
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal FixedAmount {  get; set; }
        public decimal Percentage {  get; set; }
        public int? MinQuantity { get; set; }

        public int? SpecificItemId { get; set; }

        public List<string>? CategoryNames { get; set; }

        public List<string>? LoyaltyProgramDescriptions { get; set; }

        public static explicit operator Discount(DB.Models.Discount d) => new Discount
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            ValidFrom = d.ValidFrom,
            ValidUntil = d.ValidUntil,
            FixedAmount = d.DiscountInAmount,
            Percentage = d.DiscountInPct,

            MinQuantity = d.minQuantity,

            SpecificItemId = d.ForSpecificItem?.Id,

            CategoryNames = d.ForCategories?.Select(c => c.Name).ToList(),

            LoyaltyProgramDescriptions = d.ForLoyaltyPrograms?.Select(l => l.Description).ToList(),
        };

    }
}
