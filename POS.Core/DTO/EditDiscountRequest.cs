namespace POS.Core.DTO
{
    public class EditDiscountRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal FixedAmount { get; set; }
        public decimal Percentage { get; set; }
        public int? MinQuantity { get; set; }

        public int? SpecificItemId { get; set; }

        public List<int>? CategoryIds { get; set; }

        public List<int>? LoyaltyProgramIds { get; set; }
    }
}
